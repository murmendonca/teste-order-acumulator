import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { Ativo, Lado, OrdemRequest, Resultado } from '../../core/models/ordem.model';
import { FormBuilder, Validators } from '@angular/forms';
import { OrdemService } from '../../services/ordem.service';
import { inteiro, multiploCentavo } from '../../core/validators/ordem.validators';
import { ReactiveFormsModule } from '@angular/forms';
import { CurrencyPipe, JsonPipe } from '@angular/common';

@Component({
  selector: 'app-order-generator',
  imports: [ReactiveFormsModule, CurrencyPipe, JsonPipe],
  templateUrl: './order-generator.html',
  styleUrl: './order-generator.scss',
})
export class OrderGenerator {
  private fb = inject(FormBuilder);
  private service = inject(OrdemService);

  ativos: Ativo[] = ['PETR4', 'VALE3', 'VIIA4'];
  lados = [{ valor: 'C', label: 'Compra' }, { valor: 'V', label: 'Venda' }];

  form = this.fb.nonNullable.group({
    ativo: ['' as Ativo, Validators.required],
    lado: ['' as Lado, Validators.required],
    quantidade: [null as number | null, [Validators.required, Validators.min(1), Validators.max(99999), inteiro]],
    preco: [null as number | null, [Validators.required, Validators.min(0.01), Validators.max(999.99), multiploCentavo]],
  });

  resultado = signal<Resultado | null>(null);
  erro = signal<string | null>(null);
  carregando = signal(false);

  enviar() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.carregando.set(true);
    this.resultado.set(null); this.erro.set(null);

    this.service.enviar(this.form.getRawValue() as OrdemRequest).subscribe({
      next: r => { this.resultado.set(r); this.carregando.set(false); },
      error: (e: HttpErrorResponse) => {
        if (e.error?.sucesso === false) this.resultado.set(e.error);
        else this.erro.set(typeof e.error === 'string' ? e.error : 'Falha ao comunicar com o servidor.');
        this.carregando.set(false);
      },
    });
  }
}
