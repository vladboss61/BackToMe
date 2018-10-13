import { TestBed, inject } from '@angular/core/testing';
import { HeroService } from './hero.service';
describe('HeroService', function () {
    beforeEach(function () {
        TestBed.configureTestingModule({
            providers: [HeroService]
        });
    });
    it('should be created', inject([HeroService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=hero.service.spec.js.map