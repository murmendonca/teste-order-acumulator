import { TestBed } from '@angular/core/testing';

import { Ordem } from './ordem';

describe('Ordem', () => {
  let service: Ordem;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Ordem);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
