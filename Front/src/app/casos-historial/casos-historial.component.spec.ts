import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CasosHistorialComponent } from './casos-historial.component';

describe('CasosHistorialComponent', () => {
  let component: CasosHistorialComponent;
  let fixture: ComponentFixture<CasosHistorialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CasosHistorialComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CasosHistorialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
