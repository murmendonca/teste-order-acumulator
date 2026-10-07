export type Ativo = 'PETR4' | 'VALE3' | 'VIIA4';
export type Lado = 'C' | 'V';

export interface OrdemRequest {
  ativo: Ativo;
  lado: Lado;
  quantidade: number;
  preco: number;
}

export interface Resultado {
  sucesso: boolean;
  exposicao_atual: number;
  msg_erro: string | null;
}