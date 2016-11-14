export class QueryDescriptor {
    $type: string;
    IncludeParameters: MemberNode[];
    IsMapped : boolean;
    Root: LNode;
    
    constructor() {
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.QueryDescriptor, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.Root = new EntryPointNode();
        this.IncludeParameters = [];
        this.IsMapped = false;
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
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.MemberNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.Member = member;
    }

    
}

export class ParameterNode extends LNode {
    $type: string;
    TypeName: string;
    constructor(typeName: string) {
        super();
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.ParameterNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.TypeName = typeName;
    }

    
}

export class EntryPointNode extends LNode {
    $type: string;
    constructor() {
        super();
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.EntryPointNode, Covis.Data.DynamicLinq.CQuery.Contracts";
    }
}

export class EntityNode extends MemberNode {
    $type: string;
    IsCollection: boolean;
    constructor(member: string, isCollection: boolean) {
        super(member);
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.EntityNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.IsCollection = isCollection;
    }

    
    
}

export class SortNode extends CallNode {
    $type: string;
    constructor(direction: boolean) {
        if (direction) {
            super("OrderBy");
        } else {
            super("OrderByDescending");
        }
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.SortNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        
    }



}

export class SkipNode extends BNode {
    $type: string;
    Skip: number;
    constructor(skip: number) {
        super();
        this.Skip = skip;
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.SkipNode, Covis.Data.DynamicLinq.CQuery.Contracts";

    }



}

export class TakeNode extends BNode {
    $type: string;
    Take: number;
    constructor(take: number) {
        super();
        this.Take = take;
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.TakeNode, Covis.Data.DynamicLinq.CQuery.Contracts";

    }



}

export  class BNode extends  LNode {
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
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.CallNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.Method = method;
    }
}

export class ConstantNode extends  INode {
    $type: string;
    Value: any;

    constructor(value: any) {
        super();
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.ConstantNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.Value = value;
    }
}

export class BinaryNode extends BNode {
    $type: string;
    BinaryOperator: BinaryOp;

    constructor(binaryOperator: BinaryOp) {
        super();
        this.$type = "Covis.Data.DynamicLinq.CQuery.Contracts.Model.BinaryNode, Covis.Data.DynamicLinq.CQuery.Contracts";
        this.BinaryOperator = binaryOperator;
    }
}

export enum BinaryOp {
    And,

    AndAlso,

    Or,

    OrElse,

    Equal,

    GreaterThan,

    GreaterThanOrEqual,

    LessThan,

    LessThanOrEqual
}

export enum MethodEnum {
    Contains,

    StartsWith,

    EndsWith,

    In
}