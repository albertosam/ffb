

export class Log {
  mensagem: string;
  erro: boolean = false;
  sucesso: boolean = false;

  constructor(public topo: string, public token: string, public cadeia: string, public pilha: string[]) {
  }
}

export class ValidadorLogger {
  logs: Log[] = [];

  constructor() {
  }

  limpar() {
    this.logs = [];
  }

  addErro(ponteiro: number, topo: string, token: string, cadeia: string, pilha: string[], mensagem: string) {
    let passo = new Log(topo, token, cadeia, pilha.map(a => a));
    passo.mensagem = mensagem;
    passo.erro = true;

    this.logs.push(passo);

    return passo;
  }

  addLog(ponteiro: number, topo: string, token: string, cadeia: string, pilha: string[]) {
    let passo = new Log(topo, token, cadeia, pilha.map(a => a));
    this.logs.push(passo);

    return passo;
  }
}
