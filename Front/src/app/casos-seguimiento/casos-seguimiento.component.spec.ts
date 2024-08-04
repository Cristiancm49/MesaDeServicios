import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CasosSeguimientoComponent } from './casos-seguimiento.component';

describe('CasosSeguimientoComponent', () => {
  let component: CasosSeguimientoComponent;
  let fixture: ComponentFixture<CasosSeguimientoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CasosSeguimientoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CasosSeguimientoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
