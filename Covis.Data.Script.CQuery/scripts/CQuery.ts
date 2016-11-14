module CData {
    export class CQuery<T extends IFilterDescriptor> {
        private WhereNode: CQueryDescriptor.CallNode;
        private descriptor: CQueryDescriptor.CQueryDescriptor;
        constructor(filters: T[]) {
            this.descriptor = new CQueryDescriptor.CQueryDescriptor();
            for (var i = 0; i < filters.length; i++) {
                this.buildFilter(filters[i], new CQueryDescriptor.ParameterNode());

            }
        }

        getDescriptor(): CQueryDescriptor.CQueryDescriptor {
            return this.descriptor;
        }
        private buildFilter(obj: IFilterDescriptor, path: CQueryDescriptor.LNode) {
            let properties = Object.getOwnPropertyNames(obj);
            for (let i = 0; i < properties.length; i++) {
                let property = properties[i];
                let value = obj[property];
                let operator = <IOperator<any>>value;
                let method = <IMethod<any>>value;
                if (method.method != null) {
                    //let track = new Tracker();
                    //track.path = path + "." + property;
                    //track.operator = method.method;
                    //track.value = method.filter;
                    //this.tracker.push(track);
                }
                else if (operator.value == null) {
                    let member = new CQueryDescriptor.MemberNode(property);
                    member.Left = path;
                    this.buildFilter(value, member);
                }
                else if (operator.value instanceof Array) {
                    let member = new CQueryDescriptor.MemberNode(property);
                    member.Left = path;
                    this.addMethodWhere(CQueryDescriptor.BinaryOperator.And, member, CQueryDescriptor.Methods.In, value.value);
                }
                else {
                    let member = new CQueryDescriptor.MemberNode(property);
                    member.Left = path;
                    if (CQueryDescriptor.CompareOperator[value.operator] !== undefined) {
                        this.addBinaryWhere(CQueryDescriptor.BinaryOperator.And, member, value.operator, value.value);
                    } else {
                        this.addMethodWhere(CQueryDescriptor.BinaryOperator.And, member, value.operator, value.value);
                    }
                }

                //let o = filter[properties[i]];
                //let isMethod = (<IMethod<any>>o).method !== undefined;
                //if ((<IOperator<any>>o).value !== undefined && (<IOperator<any>>o).operator !== undefined) {
                //    let operator = (<IOperator<any>>o).operator;
                //    let value = (<IOperator<any>>o).value;
                //}
                //let isInOperator = (<IInOperator<any>>o).value !== undefined;
            }
        }

        private addBinaryWhere(logicOp: CQueryDescriptor.BinaryOperator, memberNode: CQueryDescriptor.LNode, compareOp: CQueryDescriptor.CompareOperator, value: any) {
            let binaryNode = new CQueryDescriptor.BinaryNode(compareOp);
            binaryNode.Left = memberNode;
            binaryNode.Right = new CQueryDescriptor.ConstantNode(value);

            if (this.WhereNode == null) {
                this.WhereNode = new CQueryDescriptor.CallNode("Where");
                this.WhereNode.Right = binaryNode;
                this.AppendNode(this.WhereNode);
            } else {
                let andNode = new CQueryDescriptor.BinaryNode(logicOp);
                andNode.Left = this.WhereNode.Right as CQueryDescriptor.LNode;
                andNode.Right = binaryNode;

                this.WhereNode.Right = andNode;
            }


        }

        private addMethodWhere(logicOp: CQueryDescriptor.BinaryOperator, memberNode: CQueryDescriptor.LNode, method: CQueryDescriptor.Methods | CQueryDescriptor.StringMethods, value: any) {
            let methodName;
            if (CQueryDescriptor.StringMethods[method] !== undefined) {
                methodName = CQueryDescriptor.StringMethods[method].toString();
            }
            else if (CQueryDescriptor.Methods[method] !== undefined) {
                methodName = CQueryDescriptor.Methods[method].toString();
            } else {
                alert("unbekannte method" + method);
            }
            let callNode = new CQueryDescriptor.CallNode(methodName);
            callNode.Left = memberNode;
            callNode.Right = new CQueryDescriptor.ConstantNode(value);

            if (this.WhereNode == null) {
                let call = new CQueryDescriptor.CallNode("Where");
                call.Right = callNode;
                this.AppendNode(call);
            }
            else {
                let andNode = new CQueryDescriptor.BinaryNode(logicOp);
                andNode.Left = this.WhereNode.Right as CQueryDescriptor.LNode;
                andNode.Right = callNode;
                this.WhereNode.Right = andNode;
            }


        }

        private AppendNode(node: CQueryDescriptor.BNode) {
            node.Left = this.descriptor.Root;
            this.descriptor.Root = node;
        }
    }


    export interface IFilterDescriptor {

    }

    export interface IOperator<T> {
        operator: CQueryDescriptor.CompareOperator | CQueryDescriptor.StringMethods;
        value: T;
    }

    export interface IInOperator<T> {
        value: T[];
    }

    export interface IMethod<T extends IFilterDescriptor> {
        method: CQueryDescriptor.Methods;
        value: T;
    }




}


