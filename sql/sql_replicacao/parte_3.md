## 1. Qual a diferença entre a View Materializada, View e uma Tabela.

Uma `View` é uma tabela virtual, ela representa o resultado de uma query sem armazenar fisicamente os dados contidos nela. A View é uma forma alternativa de apresentar dados de uma tabela.

Uma View oferece alguns benefícios, como:
- Segurança: a visualização de algumas views podem ser permissões de algumas roles das quais não teriam acesso as tabelas que geraram essa view. Isso por si só cria uma camada de proteção e delimita o que certas roles podem ter acesso.

Além disso, a `View` é um campo read-only, isto é, não é possível fazer nenhuma alteração nos campos da view, mas é possível fazer Select.

---

Já a Materialized View é muito similar a View, no entanto, ela é armazenada fisicamente diferente da View que era um objeto dinâmico computado em `querytime`. Essa é uma das únicas diferenças da Materialized View pra View comum.

Essa diferença faz com que o uso de uma materialized view seja ligeiramente diferente da view comum. Por ser um objeto persistido fisicamente, o tempo de consulta tende a ser mais rápido já, que a materialized view não precisa executar nenhuma query para retornar seus dados.

Além disso se um usuário tem em sua base a materialized view ele não precisa de nenhuma conexão com o banco que possui a tabela original, isso gera uma disponibilidade sem necessitar da conexão com o banco original.

No entanto isso vem com um custo, evidentemente a criação de muitas materialized views cria um overhead de armazenamento com tantas copias armazenadas em disco de uma mesma tabela, além disso por ser uma espécie de snapshot da query no momento de criação, nem sempre a materialized view irá representar o estado mais atual dos dados, é necessário que a materialized view seja precomputada sempre que houver novas alterações, diferente da view que executa a query a cada chamada, o que gera uma necessidade de uma estrutura de automatização de criação de novas materialized views dependendo da demanda.

---

| Objeto | Armazenada Fisicamente | Read | Write |
| ----------- | :-----------: | :----: | :---: |
| Tabela    | Sim |  Sim | Yes |
| View      | Nao |  Sim | No | 
| Mat. View | Sim |  Sim | No |

## 2. Dentro da View Materializada, eu poderia fazer um insert? eu deveria fazer?

## 3. O que é uma arquitetura master slave?

EM SQL uma arquitetura Master Slave é um tipo de setup de replicação onde um database server atua como o Master/Original e um ou mais outros servers atuam como Slaves/Replica.

O Mestre é o responsável pelas operações de escrita e manter as cópias dos dados. Qualquer alteração sofrida pelo Mestre será replicada para os Slaves. Os servers Slaves são versões read-only dos dados do server Master, para fins de load balancing, backup, etc.

