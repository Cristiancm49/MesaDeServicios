import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CasosResueltosComponent } from './casos-resueltos.component';

describe('CasosResueltosComponent', () => {
  let component: CasosResueltosComponent;
  let fixture: ComponentFixture<CasosResueltosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CasosResueltosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CasosResueltosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
