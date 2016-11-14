module ModelDescriptors {
    export class IModel {
        $type: string;
        propertyTracker: Array<string>;
        removePropertyTracker() {
            this.clearTracker(this);
        }

        private clearTracker(metadata) {
            this.propertyTracker = undefined;
            let keys = Object.keys(Object.getPrototypeOf(metadata));
            for (let i = 0; i < keys.length; i++) {
                var property = metadata[keys[i]];
                if (property instanceof ModelDescriptors.IModel) {
                    property.propertyTracker = undefined;
                    this.clearTracker(property);
                } else if (property instanceof Array) {
                    if (property.length > 0 && property[0] instanceof IModel) {
                        property[0].propertyTracker = undefined;
                        this.clearTracker(property[0]);
                    }
                }

            }
        }

    }

    export function logProperty(target: any, key: string) {
        var _val = null;

        var getter = function () {
            if (this.propertyTracker !== undefined) {
                this.propertyTracker.push(key);
                let property = Object.getPrototypeOf(this)[key];
                if (property instanceof IModel) {
                    property.propertyTracker = this.propertyTracker;
                }
                else if (property instanceof Array) {
                    if (property.length > 0 && property[0] instanceof IModel) {
                        property[0].propertyTracker = this.propertyTracker;
                    }
                }
            }
            return _val;
        };

        var setter = function (newVal) {
            _val = newVal;
        };


        Object.defineProperty(target,
            key,
            {
                get: getter,
                set: setter,
                enumerable: true,
                configurable: true
            });

    }
}