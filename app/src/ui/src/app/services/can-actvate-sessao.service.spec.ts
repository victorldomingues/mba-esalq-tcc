import { TestBed } from '@angular/core/testing';

import { CanActvateSessaoService } from './can-actvate-sessao.service';

describe('CanActvateSessaoService', () => {
  let service: CanActvateSessaoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CanActvateSessaoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
