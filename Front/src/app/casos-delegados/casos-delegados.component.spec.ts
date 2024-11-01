import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CasosDelegadosComponent } from './casos-delegados.component';

describe('CasosDelegadosComponent', () => {
  let component: CasosDelegadosComponent;
  let fixture: ComponentFixture<CasosDelegadosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CasosDelegadosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CasosDelegadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
