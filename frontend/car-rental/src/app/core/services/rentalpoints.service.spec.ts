import { TestBed } from '@angular/core/testing';

import { RentalpointsService } from './rentalpoints.service';

describe('RentalpointsService', () => {
  let service: RentalpointsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RentalpointsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
