import { TestBed, inject } from '@angular/core/testing';
import { LoggerService } from './logger.service';
describe('LoggerService', function () {
    beforeEach(function () {
        TestBed.configureTestingModule({
            providers: [LoggerService]
        });
    });
    it('should be created', inject([LoggerService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=logger.service.spec.js.map