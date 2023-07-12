## 01. Introdução

> **Renda Fixa** - Uma classe de ativos na qual as condições de remuneração estão determinadas no momento da aplicação

O fato da remuneração já estar determinada no momento da aplicação significa que já conhecemos a rentabilidade do investimento no momento da aplicação?

R: Não necessariamente. Posso não conhecer o valor exato da correção antes do vencimento do título de renda fixa, ou posso querer resgatar meu título antes do seu vencimento.

Algumas das caracteristicas que caracterizam esse tipo de produto de Renda Fixa são:

- **Emissor** 
    - **Títulos publicos**: LTN, LFT, NTN-B
    - **Títulos de instituições privadas**: CDB, LCI, LCA, LC, debêntures
- **Vencimento do contrato**:
  - Tesouro IPCA+ 2024
  - Tesouro Prefixado 2021
- **Prefixado ou Pós-Fixado**
  - **Pré**: apenas juros, nenhuma atualização monetária
    - **Exemplos**: LTN, NTN-F
  - **Pós**: juros + correção monetária
    - **Exemplos**:  NTN-B, NTN-C, LFT
- **Valor nominal de emissão**
- **Formas de pagamento da remuneração**
  - Pagamento de cupons
    - NTN-B NTN-F pagam juros semestrais
  - Amortização do principal
    - Sem pagamento de cupons intermediários: *zero cupom*

---

## 2. Apreçamento

> Quanto vale o meu ativo? 
> 
> Como definir um preço justo para o meu ativo? Como as características acima influenciam no preço do meu contrato?

Não existe uma única maneira de avaliar o valor do meu ativo. No MITRA, podemos apreçar um título das seguintes maneiras:

- Curva PUPar
- Curva Taxa Oper
- MtM (marcação a mercado)

### PUPar

Normalmente utilizado para contratos de balcão, onde as condições de remuneração são firmadas entre as partes e não existe a possibilidade de revenda do título para terceiros, sendo o título mantido até o vencimento.

Incrementa juros e correção dia a dia desde a data de início do contrato até a data de cálculo

$$
\text{PUPar} =\text{SaldoDevedor}(DC) + \text{Juros}_{hoje} + \text{Correcao}_{hoje}
$$

---

### Taxa Oper e MtM

Para essas metodologias, avaliamos separadamente cada um dos fluxo de caixa futuros do meu título.

$$
\text{PU} = \sum_{i>DC}^{n} \text{PU}_i
$$

onde $\text{PU}_i$ é o valor presente do fluxo i.

Como determinar o VP de cada fluxo?

Valor Final de um fluxo de pagamento:
$$
\text{VF}_i = \text{Juros}_{i} + \text{Correcao}_{i} + \text{Amortizacao}_{i}
$$

Valor presente do fluxo:
$$
\text{VP}_i = \frac{\text{VF}_i}{\text{TaxaDesconto}_i}
$$


- Curva Taxa Oper
  - Normalmente utilizado para contratos que serão mantidos até o vencimento
  - Calcula-se os juros de todo o período dos fluxos de pagamento
  - Calcula-se a correção acumulada apenas até a data de cálculo
  - Todos os fluxos são descontados utilizando uma única taxa de desconto: a taxa de operação
  - Todas as informações usadas para o cálculo do preço são conhecidas até a data de cálculo

A taxa de operação é uma TIR (Taxa Interna de Retorno) calculada na data de aquisição do título e satisfaz:

$$
\text{PU}_{aq} = \sum_{i}^{n} \frac{\text{VF}_i}{(1 + \text{TIR} \%)}
$$

Ou seja, é uma taxa com qual eu desconto todos os fluxos de caixa previstos na data de aquisição e obtenho o meu preço de aquisição.


- Marcação a Mercado (MtM)
  - Normalmente utilizado para contratos que poderão ser negociados antes do seu vencimento
  - Preço do título reflete as condições atuais do mercado: esse seria seu preço caso ele fosse negociado hoje
  - Calcula-se os juros de todo o período de fluxo de pagamento
  - Calcula-se a correção de todo o período de fluxo de pagamento
    - Só conhecemos a correção até a data de cálculo. Para sabermos a correção até o vencimento do fluxo, usamos uma expectativa para o indexador (curva do indexador)
  - Algumas informações não são conhecidas na data de cálculo: utilização de curvas de mercado
  