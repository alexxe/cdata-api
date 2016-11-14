/// <reference path="../../../Covis.Data.Script.CQuery/model/IModel.ts" />
var CData;
(function (CData) {
    var Projector = (function () {
        function Projector() {
            this.projections = [];
        }
        Projector.prototype.project = function (alias, property) {
            var p = property.toString().split('.');
            var path;
            for (var i = 1; i < p.length; i++) {
                if (path === undefined) {
                    path = p[i];
                }
                else {
                    path = path + "." + p[i];
                }
            }
            var binding = new Binding(alias, new CQueryDescriptor.MemberNode(path.split(';')[0]));
            this.projections.push(binding);
        };
        Projector.prototype.getProjection = function () {
            return this.projections;
        };
        return Projector;
    }());
    CData.Projector = Projector;
    var Binding = (function () {
        function Binding(key, value) {
            this.Key = key;
            this.Value = value;
        }
        return Binding;
    }());
    CData.Binding = Binding;
    var CQuery = (function () {
        function CQuery(model, filters, projections) {
            this.descriptor = new CQueryDescriptor.CQueryDescriptor(model);
            for (var i = 0; i < filters.length; i++) {
                this.buildFilter(filters[i], "");
            }
            this.buildProjection(projections);
        }
        CQuery.prototype.buildProjection = function (bindings) {
            var node = new CQueryDescriptor.ProjectorNode();
            node.Bindings = bindings;
            node.Left = this.descriptor.Root;
            this.descriptor.Root = node;
            this.descriptor.QueryType = CQueryDescriptor.QueryType.AnonymeProjection;
        };
        CQuery.prototype.getDescriptor = function () {
            return this.descriptor;
        };
        CQuery.prototype.buildFilter = function (obj, path) {
            var properties = Object.getOwnPropertyNames(obj);
            for (var i = 0; i < properties.length; i++) {
                var property = properties[i];
                var value = obj[property];
                var operator = value;
                var method = value;
                if (method.method != null) {
                }
                else if (operator.value == null) {
                    var member = "";
                    if (path !== "") {
                        member = path + "." + property;
                    }
                    else {
                        member = property;
                    }
                    this.buildFilter(value, member);
                }
                else if (operator.value instanceof Array) {
                    var member = "";
                    if (path !== "") {
                        member = path + "." + property;
                    }
                    else {
                        member = property;
                    }
                    this.addMethodWhere(CQueryDescriptor.BinaryOperator.And, member, CQueryDescriptor.Methods.In, value.value);
                }
                else {
                    var member = "";
                    if (path !== "") {
                        member = path + "." + property;
                    }
                    else {
                        member = property;
                    }
                    if (CQueryDescriptor.CompareOperator[value.operator] !== undefined) {
                        this.addBinaryWhere(CQueryDescriptor.BinaryOperator.And, member, value.operator, value.value);
                    }
                    else {
                        this.addMethodWhere(CQueryDescriptor.BinaryOperator.And, member, value.operator, value.value);
                    }
                }
            }
        };
        CQuery.prototype.addBinaryWhere = function (logicOp, member, compareOp, value) {
            var binaryNode = new CQueryDescriptor.BinaryNode(compareOp);
            binaryNode.Left = new CQueryDescriptor.MemberNode(member);
            binaryNode.Right = new CQueryDescriptor.ConstantNode(value);
            if (this.WhereNode == null) {
                this.WhereNode = new CQueryDescriptor.CallNode("Where");
                this.WhereNode.Right = binaryNode;
                this.AppendNode(this.WhereNode);
            }
            else {
                var andNode = new CQueryDescriptor.BinaryNode(logicOp);
                andNode.Left = this.WhereNode.Right;
                andNode.Right = binaryNode;
                this.WhereNode.Right = andNode;
            }
        };
        CQuery.prototype.addMethodWhere = function (logicOp, member, method, value) {
            var methodName;
            if (CQueryDescriptor.StringMethods[method] !== undefined) {
                methodName = CQueryDescriptor.StringMethods[method].toString();
            }
            else if (CQueryDescriptor.Methods[method] !== undefined) {
                methodName = CQueryDescriptor.Methods[method].toString();
            }
            else {
                alert("unbekannte method" + method);
            }
            var callNode = new CQueryDescriptor.CallNode(methodName);
            callNode.Left = new CQueryDescriptor.MemberNode(member);
            callNode.Right = new CQueryDescriptor.ConstantNode(value);
            if (this.WhereNode == null) {
                var call = new CQueryDescriptor.CallNode("Where");
                call.Right = callNode;
                this.AppendNode(call);
            }
            else {
                var andNode = new CQueryDescriptor.BinaryNode(logicOp);
                andNode.Left = this.WhereNode.Right;
                andNode.Right = callNode;
                this.WhereNode.Right = andNode;
            }
        };
        CQuery.prototype.AppendNode = function (node) {
            node.Left = this.descriptor.Root;
            this.descriptor.Root = node;
        };
        return CQuery;
    }());
    CData.CQuery = CQuery;
})(CData || (CData = {}));
//# sourceMappingURL=CQuery.js.map