import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalEscalarInternoComponent } from './modal-escalar-interno.component';

describe('ModalEscalarInternoComponent', () => {
  let component: ModalEscalarInternoComponent;
  let fixture: ComponentFixture<ModalEscalarInternoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalEscalarInternoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalEscalarInternoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
