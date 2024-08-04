import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CasoFinalizadoComponent } from './caso-finalizado.component';

describe('CasoFinalizadoComponent', () => {
  let component: CasoFinalizadoComponent;
  let fixture: ComponentFixture<CasoFinalizadoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CasoFinalizadoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CasoFinalizadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
