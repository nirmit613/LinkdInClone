import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConnectionListsComponent } from './connection-lists.component';

describe('ConnectionListsComponent', () => {
  let component: ConnectionListsComponent;
  let fixture: ComponentFixture<ConnectionListsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConnectionListsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConnectionListsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
