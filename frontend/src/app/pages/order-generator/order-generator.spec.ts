import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderGenerator } from './order-generator';

describe('OrderGenerator', () => {
  let component: OrderGenerator;
  let fixture: ComponentFixture<OrderGenerator>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrderGenerator]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderGenerator);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
