import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CasoExcepcionalComponent } from './caso-excepcional.component';

describe('CasoExcepcionalComponent', () => {
  let component: CasoExcepcionalComponent;
  let fixture: ComponentFixture<CasoExcepcionalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CasoExcepcionalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CasoExcepcionalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
