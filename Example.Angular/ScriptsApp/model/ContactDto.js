var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Example;
(function (Example) {
    var Model;
    (function (Model) {
        var ContactDto = (function (_super) {
            __extends(ContactDto, _super);
            function ContactDto() {
                _super.call(this);
                this.type = "Example.Data.Contract.Model.ContactDto,Example.Data.Contract";
            }
            return ContactDto;
        }(CData.IModel));
        Model.ContactDto = ContactDto;
    })(Model = Example.Model || (Example.Model = {}));
})(Example || (Example = {}));
//# sourceMappingURL=ContactDto.js.map