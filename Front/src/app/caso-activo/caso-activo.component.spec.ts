import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CasoActivoComponent } from './caso-activo.component';

describe('CasoActivoComponent', () => {
  let component: CasoActivoComponent;
  let fixture: ComponentFixture<CasoActivoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CasoActivoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CasoActivoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
