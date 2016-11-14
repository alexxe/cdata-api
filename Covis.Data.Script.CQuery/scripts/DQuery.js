"use strict";
var Descriptor = require('./QueryDescriptor');
var QueryDescriptor = Descriptor.QueryDescriptor;
var MemberNode = Descriptor.MemberNode;
var CallNode = Descriptor.CallNode;
var EntityNode = Descriptor.EntityNode;
var BinaryOp = Descriptor.BinaryOp;
var MethodEnum = Descriptor.MethodEnum;
var BinaryNode = Descriptor.BinaryNode;
var ConstantNode = Descriptor.ConstantNode;
var SortNode = Descriptor.SortNode;
var SkipNode = Descriptor.SkipNode;
var TakeNode = Descriptor.TakeNode;
Array.prototype.member = function (obj) {
};
var DQuery = (function () {
    function DQuery(metadata) {
        this.descriptor = new QueryDescriptor();
        this.metadata = metadata;
    }
    DQuery.prototype.binaryFilter = function (f, op, value) {
        var trackData = this.gettrackData(f);
        var resolver = new PathResolver(this.metadata, trackData);
        var memberNode = resolver.getNode();
        this.addBinaryWhere(BinaryOp.And, memberNode, op, value);
    };
    DQuery.prototype.methodFilter = function (f, op, value) {
        var trackData = this.gettrackData(f);
        var resolver = new PathResolver(this.metadata, trackData);
        var memberNode = resolver.getNode();
        this.addMethodWhere(BinaryOp.And, memberNode, MethodEnum[op].toString(), value);
    };
    DQuery.prototype.inFilter = function (f, value) {
        var trackData = this.gettrackData(f);
        var resolver = new PathResolver(this.metadata, trackData);
        var memberNode = resolver.getNode();
        this.addMethodWhere(BinaryOp.And, memberNode, "In", value);
    };
    DQuery.prototype.dynamicMethodFilter = function (path, op, value) {
        var resolver = new PathResolver(this.metadata, path.split("."));
        var memberNode = resolver.getNode();
        this.addMethodWhere(BinaryOp.And, memberNode, MethodEnum[op].toString(), value);
    };
    DQuery.prototype.dynamicBinaryFilter = function (path, op, value) {
        var resolver = new PathResolver(this.metadata, path.split("."));
        var memberNode = resolver.getNode();
        this.addBinaryWhere(BinaryOp.And, memberNode, op, value);
    };
    DQuery.prototype.dynamicSort = function (path, isDescending, take, skip) {
        var resolver = new PathResolver(this.metadata, path.split("."));
        var memberNode = resolver.getNode();
        var node = new SortNode(!isDescending);
        node.Right = memberNode;
        this.AppendNode(node);
        if (take !== undefined) {
            var takeNode = new TakeNode(take);
            takeNode.Right = new ConstantNode(take);
            this.AppendNode(takeNode);
        }
        if (skip !== undefined) {
            var skipNode = new SkipNode(skip);
            skipNode.Right = new ConstantNode(skip);
            this.AppendNode(skipNode);
        }
    };
    DQuery.prototype.projectTo = function (param, f) {
    };
    DQuery.prototype.getDescriptor = function () {
        return this.descriptor;
    };
    DQuery.prototype.gettrackData = function (f) {
        this.metadata.propertyTracker = new Array();
        f(this.metadata);
        var trackData = this.metadata.propertyTracker;
        this.metadata.removePropertyTracker();
        return trackData;
    };
    DQuery.prototype.addBinaryWhere = function (logicOp, memberNode, compareOp, value) {
        var binaryNode = new BinaryNode(compareOp);
        binaryNode.Left = memberNode;
        binaryNode.Right = new ConstantNode(value);
        if (this.WhereNode == null) {
            this.WhereNode = new CallNode("Where");
            this.WhereNode.Right = binaryNode;
            this.AppendNode(this.WhereNode);
        }
        else {
            var andNode = new BinaryNode(logicOp);
            andNode.Left = this.WhereNode.Right;
            andNode.Right = binaryNode;
            this.WhereNode.Right = andNode;
        }
    };
    DQuery.prototype.addMethodWhere = function (logicOp, memberNode, method, value) {
        var callNode = new CallNode(method);
        callNode.Left = memberNode;
        callNode.Right = new ConstantNode(value);
        if (this.WhereNode == null) {
            var call = new CallNode("Where");
            call.Right = callNode;
            this.AppendNode(call);
        }
        else {
            var andNode = new BinaryNode(logicOp);
            andNode.Left = this.WhereNode.Right;
            andNode.Right = callNode;
            this.WhereNode.Right = andNode;
        }
    };
    DQuery.prototype.AppendNode = function (node) {
        node.Left = this.descriptor.Root;
        this.descriptor.Root = node;
    };
    return DQuery;
}());
exports.DQuery = DQuery;
var PathResolver = (function () {
    function PathResolver(metadata, path) {
        this.metadata = metadata;
        this.node = new Descriptor.ParameterNode(this.metadata.$type);
        this.resolve(this.metadata, path);
    }
    PathResolver.prototype.resolve = function (metadata, path) {
        if (path.length === 0) {
            return;
        }
        var keys = Object.keys(Object.getPrototypeOf(this.metadata));
        for (var i = 0; i < keys.length; i++) {
            if (keys[i] === path[0]) {
                var memberNode = metadata[keys[i]];
                if (memberNode instanceof ModelDescriptors.IModel) {
                    var temp = this.node;
                    var newNode = new EntityNode(keys[i], false);
                    newNode.Left = temp;
                    this.node = newNode;
                    path.shift();
                    this.resolve(memberNode, path);
                }
                else if (memberNode instanceof Array) {
                    if (memberNode.length > 0 && memberNode[0] instanceof ModelDescriptors.IModel) {
                        var temp = this.node;
                        var newNode = new EntityNode(keys[i], true);
                        newNode.Left = temp;
                        this.node = newNode;
                        path.shift();
                        this.resolve(memberNode[0], path);
                    }
                }
                else {
                    var temp = this.node;
                    var newNode = new MemberNode(keys[i]);
                    newNode.Left = temp;
                    this.node = newNode;
                }
            }
        }
    };
    PathResolver.prototype.getNode = function () {
        return this.node;
    };
    return PathResolver;
}());
//# sourceMappingURL=DQuery.js.map