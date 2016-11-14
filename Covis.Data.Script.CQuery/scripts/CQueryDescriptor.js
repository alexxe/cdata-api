var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var CQueryDescriptor;
(function (CQueryDescriptor_1) {
    var CQueryDescriptor = (function () {
        function CQueryDescriptor() {
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.QueryDescriptor, Covis.Data.DynamicLinq.CQuery.Contracts";
            this.Root = new EntryPointNode();
            this.IncludeParameters = [];
            this.IsMapped = false;
        }
        return CQueryDescriptor;
    }());
    CQueryDescriptor_1.CQueryDescriptor = CQueryDescriptor;
    var INode = (function () {
        function INode() {
            this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.INode, Covis.Data.DynamicLinq.CQuery.Contracts";
        }
        return INode;
    }());
    CQueryDescriptor_1.INode = INode;
    var LNode = (function (_super) {
        __extends(LNode, _super);
        function LNode() {
            _super.call(this);
            this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.LNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        }
        return LNode;
    }(INode));
    CQueryDescriptor_1.LNode = LNode;
    var MemberNode = (function (_super) {
        __extends(MemberNode, _super);
        function MemberNode(member) {
            _super.call(this);
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.Model.MemberNode, Covis.Data.DynamicLinq.CQuery.Contracts";
            this.Member = member;
        }
        return MemberNode;
    }(LNode));
    CQueryDescriptor_1.MemberNode = MemberNode;
    var ParameterNode = (function (_super) {
        __extends(ParameterNode, _super);
        function ParameterNode() {
            _super.call(this);
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.Model.ParameterNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        }
        return ParameterNode;
    }(LNode));
    CQueryDescriptor_1.ParameterNode = ParameterNode;
    var EntryPointNode = (function (_super) {
        __extends(EntryPointNode, _super);
        function EntryPointNode() {
            _super.call(this);
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.Model.EntryPointNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        }
        return EntryPointNode;
    }(LNode));
    CQueryDescriptor_1.EntryPointNode = EntryPointNode;
    var EntityNode = (function (_super) {
        __extends(EntityNode, _super);
        function EntityNode(member, isCollection) {
            _super.call(this, member);
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.Model.EntityNode, Covis.Data.DynamicLinq.CQuery.Contracts";
            this.IsCollection = isCollection;
        }
        return EntityNode;
    }(MemberNode));
    CQueryDescriptor_1.EntityNode = EntityNode;
    var BNode = (function (_super) {
        __extends(BNode, _super);
        function BNode() {
            _super.call(this);
            this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.BNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        }
        return BNode;
    }(LNode));
    CQueryDescriptor_1.BNode = BNode;
    var CallNode = (function (_super) {
        __extends(CallNode, _super);
        function CallNode(method) {
            _super.call(this);
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.Model.CallNode, Covis.Data.DynamicLinq.CQuery.Contracts";
            this.Method = method;
        }
        return CallNode;
    }(BNode));
    CQueryDescriptor_1.CallNode = CallNode;
    var ConstantNode = (function (_super) {
        __extends(ConstantNode, _super);
        function ConstantNode(value) {
            _super.call(this);
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.Model.ConstantNode, Covis.Data.DynamicLinq.CQuery.Contracts";
            this.Value = value;
        }
        return ConstantNode;
    }(INode));
    CQueryDescriptor_1.ConstantNode = ConstantNode;
    var BinaryNode = (function (_super) {
        __extends(BinaryNode, _super);
        function BinaryNode(binaryOperator) {
            _super.call(this);
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.Model.BinaryNode, Covis.Data.DynamicLinq.CQuery.Contracts";
            this.BinaryOperator = binaryOperator;
        }
        return BinaryNode;
    }(BNode));
    CQueryDescriptor_1.BinaryNode = BinaryNode;
    //export enum BinaryOp {
    //    And = 0,
    //    AndAlso = 1,
    //    Or = 2,
    //    OrElse = 3,
    //    Equal = 4,
    //    GreaterThan = 5,
    //    GreaterThanOrEqual = 6,
    //    LessThan = 7,
    //    LessThanOrEqual = 8
    //}
    //enum MethodEnum {
    //    Contains = 0,
    //    StartsWith = 1,
    //    EndsWith = 2,
    //    Any = 3,
    //    In = 4
    //}
    (function (BinaryOperator) {
        BinaryOperator[BinaryOperator["And"] = 0] = "And";
        BinaryOperator[BinaryOperator["AndAlso"] = 1] = "AndAlso";
        BinaryOperator[BinaryOperator["Or"] = 2] = "Or";
        BinaryOperator[BinaryOperator["OrElse"] = 3] = "OrElse";
    })(CQueryDescriptor_1.BinaryOperator || (CQueryDescriptor_1.BinaryOperator = {}));
    var BinaryOperator = CQueryDescriptor_1.BinaryOperator;
    (function (CompareOperator) {
        CompareOperator[CompareOperator["Equal"] = 4] = "Equal";
        CompareOperator[CompareOperator["GreaterThan"] = 5] = "GreaterThan";
        CompareOperator[CompareOperator["GreaterThanOrEqual"] = 6] = "GreaterThanOrEqual";
        CompareOperator[CompareOperator["LessThan"] = 7] = "LessThan";
        CompareOperator[CompareOperator["LessThanOrEqual"] = 8] = "LessThanOrEqual";
    })(CQueryDescriptor_1.CompareOperator || (CQueryDescriptor_1.CompareOperator = {}));
    var CompareOperator = CQueryDescriptor_1.CompareOperator;
    (function (StringMethods) {
        StringMethods[StringMethods["Contains"] = 9] = "Contains";
        StringMethods[StringMethods["StartsWith"] = 10] = "StartsWith";
        StringMethods[StringMethods["EndsWith"] = 11] = "EndsWith";
    })(CQueryDescriptor_1.StringMethods || (CQueryDescriptor_1.StringMethods = {}));
    var StringMethods = CQueryDescriptor_1.StringMethods;
    (function (Methods) {
        Methods[Methods["Any"] = 12] = "Any";
        Methods[Methods["In"] = 13] = "In";
    })(CQueryDescriptor_1.Methods || (CQueryDescriptor_1.Methods = {}));
    var Methods = CQueryDescriptor_1.Methods;
})(CQueryDescriptor || (CQueryDescriptor = {}));
//# sourceMappingURL=CQueryDescriptor.js.map