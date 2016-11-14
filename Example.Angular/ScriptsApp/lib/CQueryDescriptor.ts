module CQueryDescriptor {
    export class CQueryDescriptor {
        $type: string;
        IncludeParameters: MemberNode[];
        IsMapped: boolean;
        Root: LNode;
        QueryType:QueryType;

        constructor(entryPoint: CData.IModel) {
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.QueryDescriptor, Covis.Data.DynamicLinq.CQuery.Contracts";
            this.Root = new EntryPointNode(entryPoint.type);
            
            this.IncludeParameters = [];
            this.IsMapped = false;
            this.QueryType = QueryType.Default;
        }
    }

    export class INode {
        $type: string;

        constructor() {
            this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.INode, Covis.Data.DynamicLinq.CQuery.Contracts";
        }
    }

    export class LNode extends INode {
        $type: string;
        Left: LNode;

        constructor() {
            super();
            this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.LNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        }

    }


    export class MemberNode extends LNode {
        $type: string;
        Member: string;

        constructor(member: string) {
            super();
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.Model.MemberNode, Covis.Data.DynamicLinq.CQuery.Contracts";
            this.Member = member;
        }


    }

    export class EntryPointNode extends LNode {
        $type: string;
        EntryPointType:string;
        constructor(type:string) {
            super();
            this.EntryPointType = type;
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.Model.EntryPointNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        }
    }

    export class BNode extends LNode {
        $type: string;
        Right: INode;

        constructor() {
            super();
            this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.BNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        }
    }

    export class CallNode extends BNode {
        $type: string;
        Method: string;

        constructor(method: string) {
            super();
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.Model.CallNode, Covis.Data.DynamicLinq.CQuery.Contracts";
            this.Method = method;
        }
    }

    export class ConstantNode extends INode {
        $type: string;
        Value: any;

        constructor(value: any) {
            super();
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.Model.ConstantNode, Covis.Data.DynamicLinq.CQuery.Contracts";
            this.Value = value;
        }
    }

    export class ProjectorNode extends LNode {
        $type: string;
        Bindings: any;

        constructor() {
            super();
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.Model.ProjectorNode, Covis.Data.DynamicLinq.CQuery.Contracts";
            this.Bindings = [];
        }
    }

    export class BinaryNode extends BNode {
        $type: string;
        BinaryOperator: BinaryOperator | CompareOperator;

        constructor(binaryOperator: BinaryOperator | CompareOperator) {
            super();
            this
                .$type =
                "Covis.Data.DynamicLinq.CQuery.Contracts.Model.BinaryNode, Covis.Data.DynamicLinq.CQuery.Contracts";
            this.BinaryOperator = binaryOperator;
        }
    }

   
    export enum BinaryOperator {
        And = 0,
        AndAlso = 1,
        Or = 2,
        OrElse = 3
    }

    export enum CompareOperator {
        Equal = 4,
        GreaterThan = 5,
        GreaterThanOrEqual = 6,
        LessThan = 7,
        LessThanOrEqual = 8
    }

    export enum StringMethods {
        Contains = 9,
        StartsWith = 10,
        EndsWith = 11
    }

    export enum Methods {
        Any = 12,
        In = 13
    }

    export enum QueryType {
        Default = 0,
        ModelProjection = 1,
        AnonymeProjection = 2
    }

}