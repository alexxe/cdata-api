Array.prototype.sum = function (exp) {
    var result = 0;
    this.map(exp || (item), item).forEach(function (val) { return result += val; });
    return result;
};
//# sourceMappingURL=test.js.map