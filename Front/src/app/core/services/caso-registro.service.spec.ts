import { TestBed } from '@angular/core/testing';

import { CasoRegistroService } from './caso-registro.service';

describe('CasoRegistroService', () => {
  let service: CasoRegistroService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CasoRegistroService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
