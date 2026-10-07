import { AbstractControl, ValidationErrors } from '@angular/forms';

export function inteiro(c: AbstractControl): ValidationErrors | null {
  const v = c.value;
  return v === null || v === '' || Number.isInteger(v) ? null : { inteiro: true };
}

export function multiploCentavo(c: AbstractControl): ValidationErrors | null {
  const v = c.value;
  if (v === null || v === '') return null;
  return Math.abs(v * 100 - Math.round(v * 100)) < 1e-6 ? null : { multiploCentavo: true };
}