import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministrarRolComponent } from './administrar-rol.component';

describe('AdministrarRolComponent', () => {
  let component: AdministrarRolComponent;
  let fixture: ComponentFixture<AdministrarRolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdministrarRolComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministrarRolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
