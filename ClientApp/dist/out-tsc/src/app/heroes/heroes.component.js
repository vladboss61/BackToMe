var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { Http } from '@angular/http';
var HeroesComponent = /** @class */ (function () {
    function HeroesComponent(_http) {
        this._http = _http;
        this.heroes = [];
        HeroesComponent_1.Hero = "Lord";
    }
    HeroesComponent_1 = HeroesComponent;
    HeroesComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._http.get("/api/heroes").subscribe(function (values) {
            _this.heroes = values.json();
        });
    };
    var HeroesComponent_1;
    HeroesComponent = HeroesComponent_1 = __decorate([
        Component({
            selector: 'app-heroes',
            templateUrl: './heroes.component.html',
            styleUrls: ['./heroes.component.css']
        }),
        __metadata("design:paramtypes", [Http])
    ], HeroesComponent);
    return HeroesComponent;
}());
export { HeroesComponent };
//# sourceMappingURL=heroes.component.js.map