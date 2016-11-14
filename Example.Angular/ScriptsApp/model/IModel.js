var CData;
(function (CData) {
    var IModel = (function () {
        function IModel() {
        }
        IModel.prototype.removePropertyTracker = function () {
            this.clearTracker(this);
        };
        IModel.prototype.clearTracker = function (metadata) {
            this.propertyTracker = undefined;
            var keys = Object.keys(Object.getPrototypeOf(metadata));
            for (var i = 0; i < keys.length; i++) {
                var property = metadata[keys[i]];
                if (property instanceof IModel) {
                    property.propertyTracker = undefined;
                    this.clearTracker(property);
                }
                else if (property instanceof Array) {
                    if (property.length > 0 && property[0] instanceof IModel) {
                        property[0].propertyTracker = undefined;
                        this.clearTracker(property[0]);
                    }
                }
            }
        };
        return IModel;
    }());
    CData.IModel = IModel;
    function logProperty(target, key) {
        var _val = null;
        var getter = function () {
            if (this.propertyTracker !== undefined) {
                this.propertyTracker.push(key);
                var property = Object.getPrototypeOf(this)[key];
                if (property instanceof IModel) {
                    property.propertyTracker = this.propertyTracker;
                }
                else if (property instanceof Array) {
                    if (property.length > 0 && property[0] instanceof IModel) {
                        property[0].propertyTracker = this.propertyTracker;
                    }
                }
            }
            //return _val;
            return key;
        };
        var setter = function (newVal) {
            _val = newVal;
        };
        Object.defineProperty(target, key, {
            get: getter,
            set: setter,
            enumerable: true,
            configurable: true
        });
    }
    CData.logProperty = logProperty;
})(CData || (CData = {}));
//# sourceMappingURL=IModel.js.map