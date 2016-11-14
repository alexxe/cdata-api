import * as Descriptor from './QueryDescriptor';
import QueryDescriptor = Descriptor.QueryDescriptor;
import MemberNode = Descriptor.MemberNode;
import ParameterNode = Descriptor.ParameterNode;
import CallNode = Descriptor.CallNode;
import EntityNode = Descriptor.EntityNode;
import BinaryOp = Descriptor.BinaryOp;
import MethodEnum = Descriptor.MethodEnum;
import BinaryNode = Descriptor.BinaryNode;
import ConstantNode = Descriptor.ConstantNode;
import BNode = Descriptor.BNode;
import LNode = Descriptor.LNode;
import SortNode = Descriptor.SortNode;
import SkipNode = Descriptor.SkipNode;
import TakeNode = Descriptor.TakeNode;

declare global {
    interface Array<T> {
        member(f: (x: T) => void): void;
    }
}
Array.prototype.member = function (obj) {

}

export class DQuery<T extends ModelDescriptors.IModel> {
    private metadata: T;
    private WhereNode: CallNode;

    constructor(metadata: T) {
        this.descriptor = new QueryDescriptor();
        this.metadata = metadata;
    }

    private descriptor: QueryDescriptor;

    binaryFilter(f: (x: T) => void, op: BinaryOp, value: any) {
        let trackData = this.gettrackData(f);
        let resolver = new PathResolver(this.metadata, trackData);
        let memberNode = resolver.getNode();
        this.addBinaryWhere(BinaryOp.And, memberNode, op, value);
    }

    methodFilter(f: (x: T) => void, op: MethodEnum, value: any) {
        let trackData = this.gettrackData(f);
        let resolver = new PathResolver(this.metadata, trackData);
        let memberNode = resolver.getNode();
        this.addMethodWhere(BinaryOp.And, memberNode, MethodEnum[op].toString(), value);
    }

    inFilter(f: (x: T) => void,value: any) {
        let trackData = this.gettrackData(f);
        let resolver = new PathResolver(this.metadata, trackData);
        let memberNode = resolver.getNode();
        this.addMethodWhere(BinaryOp.And, memberNode,"In",value);
    }

    dynamicMethodFilter(path: string, op: MethodEnum, value: any) {
        let resolver = new PathResolver(this.metadata, path.split("."));
        let memberNode = resolver.getNode();
        this.addMethodWhere(BinaryOp.And, memberNode, MethodEnum[op].toString(), value);
    }

    dynamicBinaryFilter(path: string, op: BinaryOp, value: any) {
        let resolver = new PathResolver(this.metadata, path.split("."));
        let memberNode = resolver.getNode();
        this.addBinaryWhere(BinaryOp.And, memberNode, op, value);
    }

    dynamicSort(path: string, isDescending: boolean,take?: number,skip?: number) {
        let resolver = new PathResolver(this.metadata, path.split("."));
        let memberNode = resolver.getNode();
        let node = new SortNode(!isDescending);
        node.Right = memberNode;
        this.AppendNode(node);

        if (take !== undefined) {
            let takeNode = new TakeNode(take);
            takeNode.Right = new ConstantNode(take);
            this.AppendNode(takeNode);
        }

        if (skip !== undefined) {
            let skipNode = new SkipNode(skip);
            skipNode.Right = new ConstantNode(skip);
            this.AppendNode(skipNode);
        }
    }

    projectTo(param: string, f: (x: T) => void) {

    }

    getDescriptor() {
        return this.descriptor;
    }

    private gettrackData(f: (x: T) => void): Array<string> {
        this.metadata.propertyTracker = new Array<string>();
        f(this.metadata);
        let trackData = this.metadata.propertyTracker;
        this.metadata.removePropertyTracker();
        return trackData;
    }

    private addBinaryWhere(logicOp: BinaryOp, memberNode: Descriptor.LNode, compareOp: BinaryOp, value: any) {
        let binaryNode = new BinaryNode(compareOp);
        binaryNode.Left = memberNode;
        binaryNode.Right = new ConstantNode(value);

        if (this.WhereNode == null) {
            this.WhereNode = new CallNode("Where");
            this.WhereNode.Right = binaryNode;
            this.AppendNode(this.WhereNode);
        } else {
            let andNode = new BinaryNode(logicOp);
            andNode.Left = this.WhereNode.Right as LNode;
            andNode.Right = binaryNode;

            this.WhereNode.Right = andNode;
        }


    }

    private addMethodWhere(logicOp: BinaryOp, memberNode: Descriptor.LNode, method: string, value: any) {
        let callNode = new CallNode(method);
        callNode.Left = memberNode;
        callNode.Right = new ConstantNode(value);

        if (this.WhereNode == null) {
            let call = new CallNode("Where");
            call.Right = callNode;
            this.AppendNode(call);
        }
        else {
            let andNode = new BinaryNode(logicOp);
            andNode.Left = this.WhereNode.Right as LNode;
            andNode.Right = callNode;
            this.WhereNode.Right = andNode;
        }


    }

    private AppendNode(node: BNode) {
        node.Left = this.descriptor.Root;
        this.descriptor.Root = node;
    }

}

class PathResolver<T extends ModelDescriptors.IModel> {
    private metadata: T;
    constructor(metadata: T, path: Array<string>) {
        this.metadata = metadata;
        this.node = new Descriptor.ParameterNode(this.metadata.$type);
        this.resolve(this.metadata, path);

    }
    private node: Descriptor.LNode;

    private resolve(metadata: T, path: string[]) {
        if (path.length === 0) {
            return;
        }
        let keys = Object.keys(Object.getPrototypeOf(this.metadata));
        for (let i = 0; i < keys.length; i++) {
            if (keys[i] === path[0]) {
                var memberNode = metadata[keys[i]];
                if (memberNode instanceof ModelDescriptors.IModel) {
                    let temp = this.node;
                    let newNode = new EntityNode(keys[i], false);
                    newNode.Left = temp;
                    this.node = newNode;
                    path.shift();
                    this.resolve(memberNode, path);
                } else if (memberNode instanceof Array) {
                    if (memberNode.length > 0 && memberNode[0] instanceof ModelDescriptors.IModel) {
                        let temp = this.node;
                        let newNode = new EntityNode(keys[i], true);
                        newNode.Left = temp;
                        this.node = newNode;
                        path.shift();
                        this.resolve(memberNode[0], path);
                    }
                }
                else {
                    let temp = this.node;
                    let newNode = new MemberNode(keys[i]);
                    newNode.Left = temp;
                    this.node = newNode;


                }
            }
        }

    }

    public getNode(): Descriptor.LNode {
        return this.node;
    }
}