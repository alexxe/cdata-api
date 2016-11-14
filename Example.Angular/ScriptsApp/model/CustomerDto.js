var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Example;
(function (Example) {
    var Model;
    (function (Model) {
        var CustomerDto = (function (_super) {
            __extends(CustomerDto, _super);
            function CustomerDto() {
                _super.call(this);
                this.type = "Example.Data.Contract.Model.CustomerDto,Example.Data.Contract";
            }
            return CustomerDto;
        }(CData.IModel));
        Model.CustomerDto = CustomerDto;
    })(Model = Example.Model || (Example.Model = {}));
})(Example || (Example = {}));
//# sourceMappingURL=CustomerDto.js.map