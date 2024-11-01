import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalEscalarExternoComponent } from './modal-escalar-externo.component';

describe('ModalEscalarExternoComponent', () => {
  let component: ModalEscalarExternoComponent;
  let fixture: ComponentFixture<ModalEscalarExternoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalEscalarExternoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalEscalarExternoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
