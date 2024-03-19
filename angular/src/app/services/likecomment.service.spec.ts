import { TestBed } from '@angular/core/testing';

import { LikecommentService } from './likecomment.service';

describe('LikecommentService', () => {
  let service: LikecommentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LikecommentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
