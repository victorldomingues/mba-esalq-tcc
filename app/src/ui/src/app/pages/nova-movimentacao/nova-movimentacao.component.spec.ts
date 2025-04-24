import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NovaMovimentacaoComponent } from './nova-movimentacao.component';

describe('NovaMovimentacaoComponent', () => {
  let component: NovaMovimentacaoComponent;
  let fixture: ComponentFixture<NovaMovimentacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NovaMovimentacaoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NovaMovimentacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
