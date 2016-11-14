var CData;
(function (CData) {
    var CQuery = (function () {
        function CQuery(filters) {
            this.descriptor = new CQueryDescriptor.CQueryDescriptor();
            for (var i = 0; i < filters.length; i++) {
                this.buildFilter(filters[i], new CQueryDescriptor.ParameterNode());
            }
        }
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
                    var member = new CQueryDescriptor.MemberNode(property);
                    member.Left = path;
                    this.buildFilter(value, member);
                }
                else if (operator.value instanceof Array) {
                    var member = new CQueryDescriptor.MemberNode(property);
                    member.Left = path;
                    this.addMethodWhere(CQueryDescriptor.BinaryOperator.And, member, CQueryDescriptor.Methods.In, value.value);
                }
                else {
                    var member = new CQueryDescriptor.MemberNode(property);
                    member.Left = path;
                    if (CQueryDescriptor.CompareOperator[value.operator] !== undefined) {
                        this.addBinaryWhere(CQueryDescriptor.BinaryOperator.And, member, value.operator, value.value);
                    }
                    else {
                        this.addMethodWhere(CQueryDescriptor.BinaryOperator.And, member, value.operator, value.value);
                    }
                }
            }
        };
        CQuery.prototype.addBinaryWhere = function (logicOp, memberNode, compareOp, value) {
            var binaryNode = new CQueryDescriptor.BinaryNode(compareOp);
            binaryNode.Left = memberNode;
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
        CQuery.prototype.addMethodWhere = function (logicOp, memberNode, method, value) {
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
            callNode.Left = memberNode;
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