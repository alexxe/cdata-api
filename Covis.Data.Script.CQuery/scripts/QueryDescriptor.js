"use strict";
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var QueryDescriptor = (function () {
    function QueryDescriptor() {
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.QueryDescriptor, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.Root = new EntryPointNode();
        this.IncludeParameters = [];
        this.IsMapped = false;
    }
    return QueryDescriptor;
}());
exports.QueryDescriptor = QueryDescriptor;
var INode = (function () {
    function INode() {
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.INode, Covis.Data.DynamicLinq.CQuery.Contracts";
    }
    return INode;
}());
exports.INode = INode;
var LNode = (function (_super) {
    __extends(LNode, _super);
    function LNode() {
        _super.call(this);
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.LNode, Covis.Data.DynamicLinq.CQuery.Contracts";
    }
    return LNode;
}(INode));
exports.LNode = LNode;
var MemberNode = (function (_super) {
    __extends(MemberNode, _super);
    function MemberNode(member) {
        _super.call(this);
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.MemberNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.Member = member;
    }
    return MemberNode;
}(LNode));
exports.MemberNode = MemberNode;
var ParameterNode = (function (_super) {
    __extends(ParameterNode, _super);
    function ParameterNode(typeName) {
        _super.call(this);
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.ParameterNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.TypeName = typeName;
    }
    return ParameterNode;
}(LNode));
exports.ParameterNode = ParameterNode;
var EntryPointNode = (function (_super) {
    __extends(EntryPointNode, _super);
    function EntryPointNode() {
        _super.call(this);
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.EntryPointNode, Covis.Data.DynamicLinq.CQuery.Contracts";
    }
    return EntryPointNode;
}(LNode));
exports.EntryPointNode = EntryPointNode;
var EntityNode = (function (_super) {
    __extends(EntityNode, _super);
    function EntityNode(member, isCollection) {
        _super.call(this, member);
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.EntityNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.IsCollection = isCollection;
    }
    return EntityNode;
}(MemberNode));
exports.EntityNode = EntityNode;
var SortNode = (function (_super) {
    __extends(SortNode, _super);
    function SortNode(direction) {
        if (direction) {
            _super.call(this, "OrderBy");
        }
        else {
            _super.call(this, "OrderByDescending");
        }
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.SortNode, Covis.Data.DynamicLinq.CQuery.Contracts";
    }
    return SortNode;
}(CallNode));
exports.SortNode = SortNode;
var SkipNode = (function (_super) {
    __extends(SkipNode, _super);
    function SkipNode(skip) {
        _super.call(this);
        this.Skip = skip;
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.SkipNode, Covis.Data.DynamicLinq.CQuery.Contracts";
    }
    return SkipNode;
}(BNode));
exports.SkipNode = SkipNode;
var TakeNode = (function (_super) {
    __extends(TakeNode, _super);
    function TakeNode(take) {
        _super.call(this);
        this.Take = take;
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.TakeNode, Covis.Data.DynamicLinq.CQuery.Contracts";
    }
    return TakeNode;
}(BNode));
exports.TakeNode = TakeNode;
var BNode = (function (_super) {
    __extends(BNode, _super);
    function BNode() {
        _super.call(this);
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.BNode, Covis.Data.DynamicLinq.CQuery.Contracts";
    }
    return BNode;
}(LNode));
exports.BNode = BNode;
var CallNode = (function (_super) {
    __extends(CallNode, _super);
    function CallNode(method) {
        _super.call(this);
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.CallNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.Method = method;
    }
    return CallNode;
}(BNode));
exports.CallNode = CallNode;
var ConstantNode = (function (_super) {
    __extends(ConstantNode, _super);
    function ConstantNode(value) {
        _super.call(this);
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.ConstantNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.Value = value;
    }
    return ConstantNode;
}(INode));
exports.ConstantNode = ConstantNode;
var BinaryNode = (function (_super) {
    __extends(BinaryNode, _super);
    function BinaryNode(binaryOperator) {
        _super.call(this);
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.BinaryNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.BinaryOperator = binaryOperator;
    }
    return BinaryNode;
}(BNode));
exports.BinaryNode = BinaryNode;
(function (BinaryOp) {
    BinaryOp[BinaryOp["And"] = 0] = "And";
    BinaryOp[BinaryOp["AndAlso"] = 1] = "AndAlso";
    BinaryOp[BinaryOp["Or"] = 2] = "Or";
    BinaryOp[BinaryOp["OrElse"] = 3] = "OrElse";
    BinaryOp[BinaryOp["Equal"] = 4] = "Equal";
    BinaryOp[BinaryOp["GreaterThan"] = 5] = "GreaterThan";
    BinaryOp[BinaryOp["GreaterThanOrEqual"] = 6] = "GreaterThanOrEqual";
    BinaryOp[BinaryOp["LessThan"] = 7] = "LessThan";
    BinaryOp[BinaryOp["LessThanOrEqual"] = 8] = "LessThanOrEqual";
})(exports.BinaryOp || (exports.BinaryOp = {}));
var BinaryOp = exports.BinaryOp;
(function (MethodEnum) {
    MethodEnum[MethodEnum["Contains"] = 0] = "Contains";
    MethodEnum[MethodEnum["StartsWith"] = 1] = "StartsWith";
    MethodEnum[MethodEnum["EndsWith"] = 2] = "EndsWith";
    MethodEnum[MethodEnum["In"] = 3] = "In";
})(exports.MethodEnum || (exports.MethodEnum = {}));
var MethodEnum = exports.MethodEnum;
//# sourceMappingURL=QueryDescriptor.js.map