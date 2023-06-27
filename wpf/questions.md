## 1. O que é o WPF?
  
  WPF, ou, Windows Presentation Foundation é uma framework de UI que é independente de resolução e usa renderização baseada em vetores. WPF provê um conjunto de features que incluem o Extensible Application Markup Language (XAML), controls, data binding, layout, 2D and 3D graphics, animation, styles, templates, documents, media, text e typography. WPF é parte do .NET, então vc pode construir aplicações que incorporam outros elementos da .NET API.

  WPF permite o desenvolvimento de uma aplicação utilizando *markup* e *code-behind*. Geralmente o XAML markup é utilizado para implementar a aparência de uma aplicação enquanto usando linguagens de programação (code-behind) para implementar o seu comportamento. Essa separação de aparência e comportamento possui os seguintes benefícios:
    - os custos de desenvolvimento e manutenção são reduzidos pois o markup da aparência não está acoplado com o código do comportamento;
    - Desenvolvimento é mais eficiente pois designers podem implementar a aparencia de uma aplicação enquanto desenvolvedores estão implementando o comportamento.

  XAML é um markup baseado em XML e é o que implementa a aparencia da aplicação declarativamente. Normalmente é utilizado para definir janelas, caixas de dialogo, controle de usuário, etc. Como XAML é baseado em XML, a UI é criada em uma hierarquia de elementos aninhados, que é conhecida como *element tree*. A element tree cria um modo intuitivo e lógico de criar UI.
 
## 2. Explique o Padrão Arquitetural MVVM

  O MVVM (Model-View-ViewModel) é um padrão de design de arquitetura de interface do usuário para desacoplamento da interface do usuário e de codigo que nao é da interface do usuário. Com o MVVM, você define sua interface de usuário declarativamente usando em XAML e usa a marcação da associação de dados para vinculá-la a outras camadas que contêm dados e comandos.

  Como oferece acoplamento flexível, o uso da ssociação de dados reduz as dependências rígidas entre diferentes tipos de código. Isso facilita a alteração de unidades de código individuais sem causar efeitos indesejados em outras unidades. Esse desacoplamento é um exemplo da *separação de preocupações*, que é um conceito importante em muitos padrões de design.

  Camadas de aplicativo:
    
  - **Camada Model**  Define os tipos que representam seus dados corporativos. isso inclui tudo o que é necessário para modelar o domínio principal do aplicativo e, muitas vezes, também inclui a lógica do aplicativo principal. Essa camada é completamente independente das camadas view e view-model e, muitas vezes, reside parcialmente na nuvem. Dada uma camada **Model** totalmente impementada, você poderá criar vários aplicativos cliente diferentes, se preferir, como aplicativos UWP e Web que funcionam com os memos dados subjacentes.
  
  - **Camada View** - Define a interface do usuário usando marcação XAML. A marcação inclui expressões de associação de dados (como x:Bind) que definem a conexão entre componentes específicos da interface do usuário e vários membros de model e view-model. Os arquivos code-behind às vezes são usados como parte da camada view a fim de conter código adicional necessário para personalizar ou manipular a interface do usuário ou extrair dados de argumentos.
 
  - **Camada View-Model** - Fornece destinos de associação de dados para a exibição. Em muitos casos, view-model expões o modelo diretamente ou fornece membros que encapsulam membros específicos do modelo.
  O view-model também pode definir membros para manter o controle dos dados que forem relevantes para a interface do usuário, mas não para o modelo, como a ordem de exibição de uma lista de itens.
  O view-model também serve como um ponto dee integração com outros serviços, como o código de acesso ao banco de dados. Para projetos simples, talvez você não precisa de uma camada de modelo separada, mas apenas do view-model que encapsula todos os dados necessários.

## 3. O que é fazer um Binding?

  Data Binding é o processo que estabelece uma conexão entre a UI da aplicação e o dado que é exibido. Se o binding tem a configuração correta e o dado gera as notificações adequadas, quando o dado muda o seu valor, os elementos que estão "bindados" com esse dado sofrem alterações automaticamente. Se um usuário edita o valor em uma TextBox, o valor é automaticamente atualizado para refletir essa alteração.

  Um uso típico de data binding é colocar dados de configurações (locais ou de server) em forms ou outros controles de UI. No WPF, esse conceito é expandido para incluir no binding um número maior de propriedades para tipos diferentes de fonte de dado.

  Data binding é essencialmente a ponte entre o bind source e o bind target. Tipicamente, cada binding possui quatro componentes:
     - Binding target object
     - Target property
     - Binding source
     - Path to the value in the binding source to use

  For example, if you bound the content of a TextBox to the Employee.Name property, you would setup your binding like this:

  | Component         | Value    |
  |-------------------|----------|
  | Target            | `Textbox`  |
  | Target property   | `Text`     |
  | Source object     | `Employee` |
  | Object value path | `Name`     |

## 4. Em suas palavras, explique o Padrão de Projeto Observer.

  O padrão de Projeto Observer é um padrão comportamental utilizado no desenvolvimento de software, o padrão Observer contrém três principais componentes. O **Subject** os **Observers** e as Notificações.
  O **Subject** é o objeto principal que contém o estado que será monitorado. Ele possui uma lista de **Observers** registrados, que são os objetos interessados na alteração de estado do **Subject**.
  Os observers são notificados pelo Subject quando acontece alguma alteração no seu estado. 
  Quando o estado do Subject é alterado, ele envia uma notificação para todos os Observers registrados, indicando que houve uma mudança. Os Observers recebem essa notificação e podem atualizar-se com base nas informações fornecidas pelo Subject.

  Uma das vantagens do padrão Observer é que ele promove o desacoplamento entre os objetos. O Subject não precisa conhecer os detalhes de implementação dos Observers, apenas precisa notificá-los quando necessário. 
  Dessa forma, é possível adicionar ou remover Observers sem afetar o funcionamento do Subject.


## 5. Qual o papel da interface INotifyPropertyChanged?

  A interface INotifyPropertyChanged é utilizada para notificar quando uma propriedade de um objeto é alterada.
  O papel da interface INotifyPropertyChanged é permitir que um objeto notifique automaticamente outros objetos interessados quando o valor de uma de suas propriedades é alterado.
  Ao implementar a interface INotifyPropertyChanged em uma classe, o objeto se compromete a fornecer um evento chamado PropertyChanged. Esse evento é disparado sempre que uma propriedade do objeto é modificada. 
  Os observadores, que podem ser outros objetos, como elementos de interface gráfica (UI) ou partes do código, se registram para esse evento e recebem notificações quando ocorrem alterações nas propriedades do objeto.

## 6. Qual a diferença entre INotifyPropertyChanged e a INotifyCollectionChanged?

  A INotifyPropertyChanged é usada para notificar alterações em propriedades individuais de um objeto, enquanto a INotifyCollectionChanged é usada para notificar alterações em uma coleção de objetos como um todo.

## 7. Qual a diferença entre uma List, um ObservableCollection e um BindingList?

## 8. O que é vazamento de memória?

  Vazamento de memória, também conhecido como memory leak, ocorre quando um programa alocou espaço na memória para armazenar dados ou objetos e, posteriormente, falha em liberar esse espaço quando não é mais necessário.
  Ocorre quando há: Falha na desalocação de memória, referências retidas, ciclos de referência

## 9.  O que é Garbage Collector e como ele funciona?

  Ele é responsável por gerenciar automaticamente a alocação e liberação de memória utilizada pelos objetos criados durante a execução de um programa.
  O principal objetivo do Garbage Collector é identificar e coletar objetos que não estão mais sendo utilizados pelo programa, liberando a memória ocupada por eles. 
  Isso evita vazamentos de memória e alivia o desenvolvedor da responsabilidade de gerenciar manualmente a alocação e liberação de memória.

## 10.   Cite duas linguagens, uma que também tenha e outra que não tenha Garbage Collector.
  C e Rust são linguagens que não tem um Garbage Collector.
  C#, Java e Python são linguagens que tem um Garbage Collector.