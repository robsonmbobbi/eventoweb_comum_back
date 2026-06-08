# Plano Detalhado de Testes para Classes de Negócio

## Introdução

Este documento descreve todos os testes que devem ser implementados para as classes de negócio que herdam da classe `Entidade`. O objetivo é garantir cobertura adequada das funcionalidades, validações e regras de negócio de cada classe.

**Escopo:** Classes de negócio apenas (excluindo classes de integração e notificação por enquanto)

**Framework:** xUnit

**Padrão:** Arrange-Act-Assert (AAA)

---

## 1. Pessoa

**Localização:** `EventoWeb.Comum\Negocio\Entidades\Pessoa.cs`

**Classe Base:** `Entidade`

**Responsabilidade:** Representar uma pessoa com dados pessoais, informações de contato e características de saúde.

### 1.1 Testes de Criação com Sucesso

#### Teste 1.1.1: CriarPessoaComDadosValidos
- **Objetivo:** Verificar se uma pessoa é criada com sucesso quando todos os dados obrigatórios são fornecidos
- **Cenário:** Criar pessoa com CPF, Nome, Email e CelularWP válidos
- **Arranjo:**
  - CPF válido: "05506427654"
  - Nome: "João Silva"
  - Email: "joao@example.com"
  - Celular: "37991925134"
- **Ação:** Instanciar nova Pessoa com os dados acima
- **Asserção:**
  - Pessoa foi criada com sucesso
  - `CPF.Numero` == "05506427654"
  - `Nome.Valor` == "João Silva"
  - `Email.Endereco` == "joao@example.com"
  - `CelularWP.Numero` == "37991925134"
  - `Sexo` == null
  - `DataNascimento` == null
  - `EhVegetariano` == false (padrão)
  - `EhDiabetico` == false (padrão)
  - `UsaAdocanteDiariamente` == false (padrão)

### 1.2 Testes de Validações Obrigatórias

#### Teste 1.2.1: CriarPessoaComCPFNulo
- **Objetivo:** Validar que ArgumentNullException é lançada quando CPF é nulo
- **Ação:** Tentar criar Pessoa com CPF = null
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 1.2.2: CriarPessoaComNomeNulo
- **Objetivo:** Validar que ArgumentNullException é lançada quando Nome é nulo
- **Ação:** Tentar criar Pessoa com Nome = null
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 1.2.3: CriarPessoaComEmailNulo
- **Objetivo:** Validar que ArgumentNullException é lançada quando Email é nulo
- **Ação:** Tentar criar Pessoa com Email = null
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 1.2.4: CriarPessoaComCelularNulo
- **Objetivo:** Validar que ArgumentNullException é lançada quando CelularWP é nulo
- **Ação:** Tentar criar Pessoa com CelularWP = null
- **Asserção:** ArgumentNullException deve ser lançada

### 1.3 Testes de Setters de Propriedades Obrigatórias

#### Teste 1.3.1: AtribuirCPFNuloAoSetterDeveLancarExcecao
- **Objetivo:** Validar que setter de CPF rejeita null
- **Arranjo:** Pessoa criada com sucesso
- **Ação:** Tentar atribuir CPF = null ao setter
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 1.3.2: AtribuirNomeNuloAoSetterDeveLancarExcecao
- **Objetivo:** Validar que setter de Nome rejeita null
- **Arranjo:** Pessoa criada com sucesso
- **Ação:** Tentar atribuir Nome = null ao setter
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 1.3.3: AtribuirEmailNuloAoSetterDeveLancarExcecao
- **Objetivo:** Validar que setter de Email rejeita null
- **Arranjo:** Pessoa criada com sucesso
- **Ação:** Tentar atribuir Email = null ao setter
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 1.3.4: AtribuirCelularNuloAoSetterDeveLancarExcecao
- **Objetivo:** Validar que setter de CelularWP rejeita null
- **Arranjo:** Pessoa criada com sucesso
- **Ação:** Tentar atribuir CelularWP = null ao setter
- **Asserção:** ArgumentNullException deve ser lançada

### 1.4 Testes de Propriedades Opcionais

#### Teste 1.4.1: AtribuirSexoValido
- **Objetivo:** Verificar que Sexo pode ser definido
- **Arranjo:** Pessoa criada
- **Ação:** Definir `Sexo = EnumSexo.Masculino`
- **Asserção:** `Sexo == EnumSexo.Masculino`

#### Teste 1.4.2: AtribuirDataNascimentoValida
- **Objetivo:** Verificar que DataNascimento pode ser definida
- **Arranjo:** Pessoa criada
- **Ação:** Definir `DataNascimento = new DataAniversario(15, 6, 1990)`
- **Asserção:** `DataNascimento` não é null e data está correta

#### Teste 1.4.3: AtribuirUFValida
- **Objetivo:** Verificar que UF pode ser definida
- **Arranjo:** Pessoa criada
- **Ação:** Definir `UF = new UF("SP")`
- **Asserção:** `UF.Sigla == "SP"`

#### Teste 1.4.4: AtribuirCidadeValida
- **Objetivo:** Verificar que Cidade pode ser definida
- **Arranjo:** Pessoa criada
- **Ação:** Definir `Cidade = new String300("São Paulo")`
- **Asserção:** `Cidade.Valor == "São Paulo"`

#### Teste 1.4.5: AtribuirAlergiaAlimentarValida
- **Objetivo:** Verificar que AlergiaAlimentos pode ser definida
- **Arranjo:** Pessoa criada
- **Ação:** Definir `AlergiaAlimentos = new String100("Amendoim")`
- **Asserção:** `AlergiaAlimentos.Valor == "Amendoim"`

#### Teste 1.4.6: AtribuirBooleanoEhVegetariano
- **Objetivo:** Verificar que propriedade EhVegetariano funciona
- **Arranjo:** Pessoa criada
- **Ação:** Definir `EhVegetariano = true`
- **Asserção:** `EhVegetariano == true`

#### Teste 1.4.7: AtribuirBooleanoEhDiabetico
- **Objetivo:** Verificar que propriedade EhDiabetico funciona
- **Arranjo:** Pessoa criada
- **Ação:** Definir `EhDiabetico = true`
- **Asserção:** `EhDiabetico == true`

#### Teste 1.4.8: AtribuirBooleanoUsaAdocanteDiariamente
- **Objetivo:** Verificar que propriedade UsaAdocanteDiariamente funciona
- **Arranjo:** Pessoa criada
- **Ação:** Definir `UsaAdocanteDiariamente = true`
- **Asserção:** `UsaAdocanteDiariamente == true`

### 1.5 Testes de Nullabilidade de Opcionais

#### Teste 1.5.1: SexoNuloEhValido
- **Objetivo:** Verificar que Sexo pode ser null
- **Arranjo:** Pessoa criada
- **Ação:** Definir `Sexo = null`
- **Asserção:** `Sexo == null`

#### Teste 1.5.2: DataNascimentoNulaEhValida
- **Objetivo:** Verificar que DataNascimento pode ser null
- **Arranjo:** Pessoa criada
- **Ação:** Definir `DataNascimento = null`
- **Asserção:** `DataNascimento == null`

#### Teste 1.5.3: UFNulaEhValida
- **Objetivo:** Verificar que UF pode ser null
- **Arranjo:** Pessoa criada
- **Ação:** Definir `UF = null`
- **Asserção:** `UF == null`

#### Teste 1.5.4: CidadeNulaEhValida
- **Objetivo:** Verificar que Cidade pode ser null
- **Arranjo:** Pessoa criada
- **Ação:** Definir `Cidade = null`
- **Asserção:** `Cidade == null`

#### Teste 1.5.5: AlergiaAlimentarNulaEhValida
- **Objetivo:** Verificar que AlergiaAlimentos pode ser null
- **Arranjo:** Pessoa criada
- **Ação:** Definir `AlergiaAlimentos = null`
- **Asserção:** `AlergiaAlimentos == null`

### 1.6 Testes de Igualdade (Herança de Entidade)

#### Teste 1.6.1: DuasPessoasSemIdSaoIguaisSeForemOMesmoObjeto
- **Objetivo:** Verificar comportamento de Equals quando ambas sem ID persistido
- **Arranjo:** Duas pessoas criadas sem ID persistido
- **Ação:** Comparar `pessoa1.Equals(pessoa1)`
- **Asserção:** São iguais (mesma referência)

#### Teste 1.6.2: DuasPessoasComIdSaoIguaisSeTiveremMesmoId
- **Objetivo:** Verificar que objetos com mesmo ID são iguais
- **Cenário:** Simular duas instâncias com mesmo ID via reflection ou mock
- **Asserção:** São iguais

---

## 2. Evento

**Localização:** `EventoWeb.Comum\Negocio\Entidades\Evento.cs`

**Classe Base:** `Entidade`

**Responsabilidade:** Representar um evento com períodos de inscrição e realização.

### 2.1 Testes de Criação com Sucesso

#### Teste 2.1.1: CriarEventoComDadosValidos
- **Objetivo:** Criar evento com dados obrigatórios válidos
- **Arranjo:**
  - Nome: "Congresso de Espiritismo 2024"
  - PeriodoInscricaoOnline: Período de 01/01/2024 a 30/06/2024
  - PeriodoRealizacaoEvento: Período de 15/07/2024 a 20/07/2024
- **Ação:** Instanciar novo Evento
- **Asserção:**
  - Evento criado com sucesso
  - `Nome.Valor` == "Congresso de Espiritismo 2024"
  - `PeriodoInscricaoOnLine` definido corretamente
  - `PeriodoRealizacaoEvento` definido corretamente
  - `DataRegistro` == DateTime.Today
  - `IdadeMinimaInscricaoAdulto.Valor` == 13
  - `Logotipo` == null
  - `Regulamento` == null

### 2.2 Testes de Validações Obrigatórias

#### Teste 2.2.1: CriarEventoComNomeNulo
- **Objetivo:** Validar que ArgumentNullException é lançada quando Nome é nulo
- **Ação:** Tentar criar Evento com Nome = null
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 2.2.2: CriarEventoComPeriodoInscricaoNulo
- **Objetivo:** Validar que ArgumentNullException é lançada quando PeriodoInscricaoOnLine é nulo
- **Ação:** Tentar criar Evento com PeriodoInscricaoOnLine = null
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 2.2.3: CriarEventoComPeriodoRealizacaoNulo
- **Objetivo:** Validar que ArgumentNullException é lançada quando PeriodoRealizacaoEvento é nulo
- **Ação:** Tentar criar Evento com PeriodoRealizacaoEvento = null
- **Asserção:** ArgumentNullException deve ser lançada

### 2.3 Testes de Setters de Propriedades

#### Teste 2.3.1: AlterarNomeValido
- **Objetivo:** Verificar que Nome pode ser alterado
- **Arranjo:** Evento criado
- **Ação:** Atribuir novo nome: `Nome = new String200("Novo Nome")`
- **Asserção:** `Nome.Valor == "Novo Nome"`

#### Teste 2.3.2: AtribuirNomeNuloDeveLancarExcecao
- **Objetivo:** Validar que setter de Nome rejeita null
- **Arranjo:** Evento criado
- **Ação:** Tentar `Nome = null`
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 2.3.3: AlterarPeriodoInscricaoValido
- **Objetivo:** Verificar que PeriodoInscricaoOnLine pode ser alterado
- **Arranjo:** Evento criado
- **Ação:** Atribuir novo período
- **Asserção:** Período alterado com sucesso

#### Teste 2.3.4: AtribuirPeriodoInscricaoNuloDeveLancarExcecao
- **Objetivo:** Validar que setter rejeita null
- **Ação:** Tentar `PeriodoInscricaoOnLine = null`
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 2.3.5: AlterarPeriodoRealizacaoValido
- **Objetivo:** Verificar que PeriodoRealizacaoEvento pode ser alterado
- **Arranjo:** Evento criado
- **Ação:** Atribuir novo período
- **Asserção:** Período alterado com sucesso

#### Teste 2.3.6: AtribuirPeriodoRealizacaoNuloDeveLancarExcecao
- **Objetivo:** Validar que setter rejeita null
- **Ação:** Tentar `PeriodoRealizacaoEvento = null`
- **Asserção:** ArgumentNullException deve ser lançada

### 2.4 Testes de Propriedades Opcionais

#### Teste 2.4.1: AtribuirLogotipo
- **Objetivo:** Verificar que Logotipo pode ser definido
- **Arranjo:** Evento criado, arquivo binário criado
- **Ação:** `evento.Logotipo = arquivoBinario`
- **Asserção:** `Logotipo` é igual ao arquivo atribuído

#### Teste 2.4.2: LogotipoNuloEhValido
- **Objetivo:** Verificar que Logotipo pode ser null
- **Ação:** `evento.Logotipo = null`
- **Asserção:** `Logotipo == null`

#### Teste 2.4.3: AtribuirRegulamento
- **Objetivo:** Verificar que Regulamento pode ser definido
- **Arranjo:** Evento criado
- **Ação:** `evento.Regulamento = new StringClob("Texto do regulamento")`
- **Asserção:** Regulamento definido corretamente

#### Teste 2.4.4: RegulamentoNuloEhValido
- **Objetivo:** Verificar que Regulamento pode ser null
- **Ação:** `evento.Regulamento = null`
- **Asserção:** `Regulamento == null`

#### Teste 2.4.5: AlterarIdadeMinimaInscricao
- **Objetivo:** Verificar que IdadeMinimaInscricaoAdulto pode ser alterada
- **Arranjo:** Evento criado
- **Ação:** `evento.IdadeMinimaInscricaoAdulto = new InteiroPositivo(18)`
- **Asserção:** `IdadeMinimaInscricaoAdulto.Valor == 18`

### 2.5 Testes de Data de Registro

#### Teste 2.5.1: DataRegistroEhDefinidaAutomaticamente
- **Objetivo:** Verificar que DataRegistro é definida como DateTime.Today na criação
- **Arranjo:** Data atual conhecida
- **Ação:** Criar evento
- **Asserção:** `evento.DataRegistro == DateTime.Today`

#### Teste 2.5.2: DataRegistroEhSomenteLeitura
- **Objetivo:** Verificar que DataRegistro não pode ser alterada (read-only)
- **Arranjo:** Evento criado
- **Asserção:** Propriedade não possui setter público

---

## 3. Inscricao e Subclasses (InscricaoParticipante, InscricaoInfantil)

**Localização:** 
- `EventoWeb.Comum\Negocio\Entidades\Inscricao.cs` (Abstrata)
- `EventoWeb.Comum\Negocio\Entidades\InscricaoParticipante.cs`
- `EventoWeb.Comum\Negocio\Entidades\InscricaoInfantil.cs`

**Classe Base:** `Entidade` (Inscricao é abstrata)

**Responsabilidade:** Gerenciar inscrições de pessoas em eventos com validação de idade e estados.

### 3.1 InscricaoParticipante - Testes de Criação

#### Teste 3.1.1: CriarInscricaoParticipanteComDadosValidos
- **Objetivo:** Criar inscrição de participante com sucesso
- **Arranjo:**
  - Evento criado
  - Pessoa com idade >= 13 anos (IdadeMinimaInscricaoAdulto padrão)
  - DataNascimento definida
  - Data de recebimento: DateTime.Now
- **Ação:** Criar nova InscricaoParticipante
- **Asserção:**
  - Inscrição criada com sucesso
  - `Pessoa` definida
  - `Evento` definido
  - `DataRecebimento` definida
  - `Situacao == EnumSituacaoInscricao.Limbo`
  - `ConfirmadoNoEvento == false`
  - `DormeEvento == true`
  - `NomeCracha == null`
  - `Observacoes == null`

#### Teste 3.1.2: CriarInscricaoParticipanteComPessoaNula
- **Objetivo:** Validar que ArgumentNullException é lançada quando Pessoa é nula
- **Ação:** Tentar criar InscricaoParticipante com Pessoa = null
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 3.1.3: CriarInscricaoParticipanteComEventoNulo
- **Objetivo:** Validar que ArgumentNullException é lançada quando Evento é nulo
- **Ação:** Tentar criar InscricaoParticipante com Evento = null
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 3.1.4: CriarInscricaoParticipanteComIdadeInsuficiente
- **Objetivo:** Validar que ArgumentException é lançada quando idade < IdadeMinimaInscricaoAdulto
- **Arranjo:**
  - Evento com IdadeMinimaInscricaoAdulto = 13
  - Pessoa com 12 anos
- **Ação:** Tentar criar InscricaoParticipante
- **Asserção:** ArgumentException deve ser lançada com mensagem apropriada

#### Teste 3.1.5: CriarInscricaoParticipanteComIdadeIgualAoMinimo
- **Objetivo:** Verificar que inscrição é aceita quando idade == IdadeMinimaInscricaoAdulto
- **Arranjo:**
  - Evento com IdadeMinimaInscricaoAdulto = 13
  - Pessoa com exatamente 13 anos
- **Ação:** Criar InscricaoParticipante
- **Asserção:** Inscrição criada com sucesso

#### Teste 3.1.6: CriarInscricaoParticipanteComIdadeSuperiorAoMinimo
- **Objetivo:** Verificar que inscrição é aceita quando idade > IdadeMinimaInscricaoAdulto
- **Arranjo:**
  - Evento com IdadeMinimaInscricaoAdulto = 13
  - Pessoa com 25 anos
- **Ação:** Criar InscricaoParticipante
- **Asserção:** Inscrição criada com sucesso

### 3.2 InscricaoParticipante - Testes de Propriedades

#### Teste 3.2.1: AtribuirTipoParticipanteValido
- **Objetivo:** Verificar que Tipo pode ser definido
- **Arranjo:** InscricaoParticipante criada
- **Ação:** `inscricao.Tipo = EnumTipoParticipante.Participante`
- **Asserção:** `Tipo == EnumTipoParticipante.Participante`

#### Teste 3.2.2: AtribuirInstituicoesEspiritasFrequenta
- **Objetivo:** Verificar que InstituicoesEspiritasFrequenta pode ser definido
- **Arranjo:** InscricaoParticipante criada
- **Ação:** `inscricao.InstituicoesEspiritasFrequenta = new String300("Instituição A")`
- **Asserção:** InstituicoesEspiritasFrequenta definido

#### Teste 3.2.3: InstituicoesEspiritasFrequentaNuloEhValido
- **Objetivo:** Verificar que InstituicoesEspiritasFrequenta pode ser null
- **Ação:** `inscricao.InstituicoesEspiritasFrequenta = null`
- **Asserção:** `InstituicoesEspiritasFrequenta == null`

#### Teste 3.2.4: AtribuirNomeCracha
- **Objetivo:** Verificar que NomeCracha pode ser definido
- **Arranjo:** InscricaoParticipante criada
- **Ação:** `inscricao.NomeCracha = new String200("João da Silva")`
- **Asserção:** NomeCracha definido

#### Teste 3.2.5: AtribuirObservacoes
- **Objetivo:** Verificar que Observacoes pode ser definida
- **Arranjo:** InscricaoParticipante criada
- **Ação:** `inscricao.Observacoes = new StringClob("Observação teste")`
- **Asserção:** Observacoes definida

#### Teste 3.2.6: AtribuirDormeEvento
- **Objetivo:** Verificar que DormeEvento pode ser alterado
- **Arranjo:** InscricaoParticipante criada
- **Ação:** `inscricao.DormeEvento = false`
- **Asserção:** `DormeEvento == false`

#### Teste 3.2.7: AtribuirConfirmadoNoEvento
- **Objetivo:** Verificar que ConfirmadoNoEvento pode ser alterado
- **Arranjo:** InscricaoParticipante criada
- **Ação:** `inscricao.ConfirmadoNoEvento = true`
- **Asserção:** `ConfirmadoNoEvento == true`

### 3.3 Testes de Máquina de Estados de Inscrição

#### Teste 3.3.1: TornarInscricaoPendente
- **Objetivo:** Verificar transição de Limbo para Pendente
- **Arranjo:** InscricaoParticipante em estado Limbo
- **Ação:** `inscricao.TornarPendente()`
- **Asserção:** `Situacao == EnumSituacaoInscricao.Pendente`

#### Teste 3.3.2: TornarPendenteAPartirDeNaoLimbo_DeveLancarExcecao
- **Objetivo:** Validar que TornarPendente só funciona de Limbo
- **Arranjo:** InscricaoParticipante em estado Pendente
- **Ação:** Tentar `inscricao.TornarPendente()`
- **Asserção:** Exception deve ser lançada com mensagem apropriada

#### Teste 3.3.3: AceitarInscricao
- **Objetivo:** Verificar transição de Pendente para Aceita
- **Arranjo:**
  - InscricaoParticipante em estado Pendente
  - `Tipo` definido
- **Ação:** `inscricao.Aceitar()`
- **Asserção:** `Situacao == EnumSituacaoInscricao.Aceita`

#### Teste 3.3.4: AceitarSemDefinirTipo_DeveLancarExcecao
- **Objetivo:** Validar que Aceitar falha se Tipo não está definido
- **Arranjo:**
  - InscricaoParticipante em estado Pendente
  - `Tipo` não foi definido (null)
- **Ação:** Tentar `inscricao.Aceitar()`
- **Asserção:** ArgumentException deve ser lançada

#### Teste 3.3.5: AceitarAPartirDeNaoPendente_DeveLancarExcecao
- **Objetivo:** Validar que Aceitar só funciona de Pendente
- **Arranjo:** InscricaoParticipante em estado Limbo
- **Ação:** Tentar `inscricao.Aceitar()`
- **Asserção:** Exception deve ser lançada

#### Teste 3.3.6: RejeitarInscricao
- **Objetivo:** Verificar transição de Pendente para Rejeitada
- **Arranjo:** InscricaoParticipante em estado Pendente
- **Ação:** `inscricao.Rejeitar()`
- **Asserção:** `Situacao == EnumSituacaoInscricao.Rejeitada`

#### Teste 3.3.7: RejeitarAPartirDeNaoPendente_DeveLancarExcecao
- **Objetivo:** Validar que Rejeitar só funciona de Pendente
- **Arranjo:** InscricaoParticipante em estado Limbo
- **Ação:** Tentar `inscricao.Rejeitar()`
- **Asserção:** Exception deve ser lançada

### 3.4 InscricaoInfantil - Testes de Criação

#### Teste 3.4.1: CriarInscricaoInfantilComDadosValidos
- **Objetivo:** Criar inscrição infantil com sucesso
- **Arranjo:**
  - Evento com IdadeMinimaInscricaoAdulto = 13
  - Pessoa criança (idade < 13)
  - DataNascimento definida
  - Dois InscricaoParticipante válidos como responsáveis
  - DormeEvento = true
- **Ação:** Criar InscricaoInfantil
- **Asserção:**
  - Inscrição criada com sucesso
  - `Pessoa` definida
  - `Evento` definido
  - `Situacao == EnumSituacaoInscricao.Limbo`
  - `DormeEvento == true`
  - `InscricaoResponsavel1` definida
  - `InscricaoResponsavel2` definida

#### Teste 3.4.2: CriarInscricaoInfantilComIdadeSuperiorAoMinimo_DeveLancarExcecao
- **Objetivo:** Validar que criança com idade >= IdadeMinimaInscricaoAdulto não é aceita
- **Arranjo:**
  - Evento com IdadeMinimaInscricaoAdulto = 13
  - Pessoa com 13 anos ou mais
- **Ação:** Tentar criar InscricaoInfantil
- **Asserção:** ArgumentException deve ser lançada

#### Teste 3.4.3: CriarInscricaoInfantilComResponsaveisDeEventosDiferentes
- **Objetivo:** Validar que responsáveis devem ser do mesmo evento
- **Arranjo:**
  - Evento A com criança
  - Responsável1 do Evento A
  - Responsável2 do Evento B (diferente)
- **Ação:** Tentar criar InscricaoInfantil
- **Asserção:** ArgumentException deve ser lançada

#### Teste 3.4.4: CriarInscricaoInfantilComResponsaveisDuplicados
- **Objetivo:** Validar que os dois responsáveis devem ser diferentes
- **Arranjo:**
  - Mesmo responsável passado duas vezes
- **Ação:** Tentar criar InscricaoInfantil
- **Asserção:** ArgumentException deve ser lançada

#### Teste 3.4.5: CriarInscricaoInfantilComUmResponsavelApenasQuandoNaoDorme
- **Objetivo:** Verificar que criança que não dorme pode ter apenas 1 responsável
- **Arranjo:**
  - DormeEvento = false
  - Um responsável com DormeEvento = false
- **Ação:** Criar InscricaoInfantil(responsavel1, null, ...)
- **Asserção:** Criada com sucesso

#### Teste 3.4.6: CriarInscricaoInfantilQueDormeComResponsaveisSemDormirDeveLancarExcecao
- **Objetivo:** Validar que se criança dorme, pelo menos um responsável deve dormir
- **Arranjo:**
  - DormeEvento = true
  - Responsável1.DormeEvento = false
  - Responsável2.DormeEvento = false
- **Ação:** Tentar criar InscricaoInfantil
- **Asserção:** ArgumentException deve ser lançada

#### Teste 3.4.7: CriarInscricaoInfantilQueDormeComPeloMenosUmResponsavelDormindo
- **Objetivo:** Verificar que criança que dorme precisa de ao menos 1 responsável dormindo
- **Arranjo:**
  - DormeEvento = true
  - Responsável1.DormeEvento = true
  - Responsável2.DormeEvento = false
- **Ação:** Criar InscricaoInfantil
- **Asserção:** Criada com sucesso

### 3.5 InscricaoInfantil - Testes de Aceitação

#### Teste 3.5.1: AceitarInscricaoInfantilComResponsaveisPendentes_DeveLancarExcecao
- **Objetivo:** Validar que inscrição infantil não pode ser aceita sem responsáveis aceitos
- **Arranjo:**
  - InscricaoInfantil em estado Pendente
  - ResponsavelPrincipal em estado Limbo
  - ResponsavelSecundário (se houver) em estado Limbo
- **Ação:** Tentar `incricaoInfantil.Aceitar()`
- **Asserção:** Exception deve ser lançada

#### Teste 3.5.2: AceitarInscricaoInfantilComResponsavelPrincipalAceito
- **Objetivo:** Verificar que inscrição infantil é aceita se responsável principal está aceito
- **Arranjo:**
  - InscricaoInfantil em estado Pendente
  - ResponsavelPrincipal em estado Aceita
- **Ação:** `incricaoInfantil.Aceitar()`
- **Asserção:** `Situacao == EnumSituacaoInscricao.Aceita`

#### Teste 3.5.3: AceitarInscricaoInfantilComResponsavelSecundarioAceito
- **Objetivo:** Verificar que inscrição infantil é aceita se responsável secundário está aceito
- **Arranjo:**
  - InscricaoInfantil em estado Pendente
  - ResponsavelPrincipal em estado Limbo
  - ResponsavelSecundário em estado Aceita
- **Ação:** `incricaoInfantil.Aceitar()`
- **Asserção:** `Situacao == EnumSituacaoInscricao.Aceita`

### 3.6 InscricaoInfantil - Testes de Reatribuição de Responsáveis

#### Teste 3.6.1: ReatribuirResponsaveisValidos
- **Objetivo:** Verificar que responsáveis podem ser reatribuídos
- **Arranjo:**
  - InscricaoInfantil com responsáveis
  - Novos responsáveis válidos
- **Ação:** `incricaoInfantil.AtribuirResponsaveis(novoResp1, novoResp2)`
- **Asserção:** Responsáveis atualizados

#### Teste 3.6.2: ReatribuirComResponsaveisDiferentesFalha
- **Objetivo:** Validar rejeição de dois responsáveis iguais na reatribuição
- **Arranjo:** InscricaoInfantil com responsáveis
- **Ação:** Tentar `AtribuirResponsaveis(mesmoResp, mesmoResp)`
- **Asserção:** ArgumentException deve ser lançada

---

## 4. Pedido

**Localização:** `EventoWeb.Comum\Negocio\Entidades\Pedido.cs`

**Classe Base:** `Entidade`

**Responsabilidade:** Representar um pedido de inscrição com validações de estado e tipo.

### 4.1 Testes de Criação com Sucesso

#### Teste 4.1.1: CriarPedidoDeDebitoComDadosValidos
- **Objetivo:** Criar pedido do tipo Débito com sucesso
- **Arranjo:**
  - Pagador: Pessoa válida
  - Inscricoes: Lista com InscricaoParticipante em estado Limbo
  - Valor: ValorMonetario(100.00)
  - Tipo: EnumTipoPedido.Debito
  - FormaPagamento: FormaPagamento válida
  - Motivo: null
- **Ação:** Criar novo Pedido
- **Asserção:**
  - Pedido criado com sucesso
  - `Pagador` definido
  - `Tipo == EnumTipoPedido.Debito`
  - `FormaPagamento` definido
  - `Valor` definido
  - `Inscricoes` contém a inscrição
  - `Conta` foi criada (Receita)
  - `Motivo == null`

#### Teste 4.1.2: CriarPedidoDeDescontoComDadosValidos
- **Objetivo:** Criar pedido do tipo Desconto com sucesso
- **Arranjo:**
  - Tipo: EnumTipoPedido.Desconto
  - FormaPagamento: null (não é obrigatória para desconto)
- **Ação:** Criar novo Pedido
- **Asserção:** Pedido criado com sucesso

#### Teste 4.1.3: CriarPedidoDeIsencaoComDadosValidos
- **Objetivo:** Criar pedido do tipo Isenção com sucesso
- **Arranjo:**
  - Tipo: EnumTipoPedido.Isencao
  - FormaPagamento: null
- **Ação:** Criar novo Pedido
- **Asserção:** Pedido criado com sucesso

### 4.2 Testes de Validações

#### Teste 4.2.1: CriarPedidoComInscricoesVaziaDeveLancarExcecao
- **Objetivo:** Validar que lista de inscrições não pode estar vazia
- **Arranjo:** Inscricoes: lista vazia
- **Ação:** Tentar criar Pedido
- **Asserção:** Exception deve ser lançada com mensagem apropriada

#### Teste 4.2.2: CriarPedidoComInscricaoEmEstadoNaoLimboDeveLancarExcecao
- **Objetivo:** Validar que inscrições devem estar em estado Limbo
- **Arranjo:**
  - Inscricao em estado Pendente (não Limbo)
- **Ação:** Tentar criar Pedido
- **Asserção:** Exception deve ser lançada com mensagem apropriada

#### Teste 4.2.3: CriarPedidoDeDebitoSemFormaPagementoDeveLancarExcecao
- **Objetivo:** Validar que Débito requer FormaPagamento
- **Arranjo:**
  - Tipo: EnumTipoPedido.Debito
  - FormaPagamento: null
- **Ação:** Tentar criar Pedido
- **Asserção:** Exception deve ser lançada com mensagem apropriada

#### Teste 4.2.4: CriarPedidoComPagadorNuloDeveLancarExcecao
- **Objetivo:** Validar que Pagador não pode ser nulo
- **Arranjo:** Pagador = null
- **Ação:** Tentar criar Pedido
- **Asserção:** Exception deve ser lançada

#### Teste 4.2.5: CriarPedidoComValorNuloDeveLancarExcecao
- **Objetivo:** Validar que Valor não pode ser nulo
- **Arranjo:** Valor = null
- **Ação:** Tentar criar Pedido
- **Asserção:** Exception deve ser lançada

### 4.3 Testes de Múltiplas Inscrições

#### Teste 4.3.1: CriarPedidoComMultiplasInscricoes
- **Objetivo:** Verificar que pedido pode conter múltiplas inscrições
- **Arranjo:**
  - 3 InscricaoParticipante em estado Limbo
- **Ação:** Criar Pedido com todas as 3 inscrições
- **Asserção:**
  - Pedido criado
  - `Inscricoes.Count() == 3`

#### Teste 4.3.2: PedidoComMultiplasInscricaosMisturadas_AlgumasPendentes
- **Objetivo:** Validar que todas inscrições devem estar em Limbo
- **Arranjo:**
  - 2 InscricaoParticipante em Limbo
  - 1 InscricaoParticipante em Pendente
- **Ação:** Tentar criar Pedido
- **Asserção:** Exception deve ser lançada

### 4.4 Testes de Conta Associada

#### Teste 4.4.1: CriarPedidoAlocaConta
- **Objetivo:** Verificar que Conta é criada ao criar Pedido
- **Arranjo:** Pedido válido
- **Ação:** Criar Pedido
- **Asserção:**
  - `Conta` não é null
  - `Conta.Pessoa == Pagador`
  - `Conta.Tipo == EnumTipoTransacao.Receita`
  - `Conta.Valor == Pedido.Valor`

---

## 5. PrecoInscricao

**Localização:** `EventoWeb.Comum\Negocio\Entidades\PrecoInscricao.cs`

**Classe Base:** `Entidade`

**Responsabilidade:** Gerenciar faixas de preço por idade com valores por forma de pagamento.

### 5.1 Testes de Criação

#### Teste 5.1.1: CriarPrecoInscricaoComDadosValidos
- **Objetivo:** Criar PrecoInscricao com sucesso
- **Arranjo:**
  - Evento: evento válido
  - IdadeMax: InteiroPositivo(17)
- **Ação:** Criar novo PrecoInscricao
- **Asserção:**
  - PrecoInscricao criada com sucesso
  - `Evento` definido
  - `IdadeMax.Valor == 17`
  - `Valores` está vazia

#### Teste 5.1.2: CriarPrecoInscricaoComEventoNulo
- **Objetivo:** Validar que Evento não pode ser nulo
- **Ação:** Tentar criar PrecoInscricao com Evento = null
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 5.1.3: CriarPrecoInscricaoComIdadeMaxNula
- **Objetivo:** Validar que IdadeMax não pode ser nula
- **Ação:** Tentar criar PrecoInscricao com IdadeMax = null
- **Asserção:** ArgumentNullException deve ser lançada

### 5.2 Testes de Adição de Valores

#### Teste 5.2.1: AdicionarValorComDadosValidos
- **Objetivo:** Adicionar valor para forma de pagamento com sucesso
- **Arranjo:**
  - PrecoInscricao criada
  - FormaPagamento válida
  - Valor: 150.00
- **Ação:** `precoInscricao.AdicionarValor(formaPagamento, 150.00)`
- **Asserção:**
  - Valor adicionado
  - `Valores.Count() == 1`
  - `Valores.First().Forma == formaPagamento`
  - `Valores.First().Valor.Valor == 150.00`

#### Teste 5.2.2: AdicionarMultiplosValoresComFormasPagementoDiferentes
- **Objetivo:** Adicionar múltiplos valores para diferentes formas
- **Arranjo:**
  - 3 FormaPagamento diferentes
- **Ação:** Adicionar valor para cada forma
- **Asserção:** `Valores.Count() == 3`

#### Teste 5.2.3: AdicionarValorParaMesmaFormaPagamentoDuasVezesDeveLancarExcecao
- **Objetivo:** Validar que não pode haver dois valores para mesma forma
- **Arranjo:**
  - PrecoInscricao com um valor adicionado
  - Mesma FormaPagamento
- **Ação:** Tentar adicionar outro valor para mesma forma
- **Asserção:** Exception deve ser lançada

### 5.3 Testes de Remoção de Valores

#### Teste 5.3.1: RemoverValorExistente
- **Objetivo:** Remover valor da lista com sucesso
- **Arranjo:**
  - PrecoInscricao com 2 valores adicionados
  - Referência para um deles
- **Ação:** `precoInscricao.RemoverValor(preco1)`
- **Asserção:**
  - Valor removido
  - `Valores.Count() == 1`

#### Teste 5.3.2: RemoverValorInexistenteDeveLancarExcecao
- **Objetivo:** Validar que remoção de valor inexistente falha
- **Arranjo:**
  - PrecoInscricao com valores
  - PrecoInscricaoValor não pertencente à lista
- **Ação:** Tentar remover valor inexistente
- **Asserção:** Exception deve ser lançada com mensagem apropriada

#### Teste 5.3.3: RemoverTodosOsValores
- **Objetivo:** Verificar que todos os valores podem ser removidos
- **Arranjo:**
  - PrecoInscricao com 3 valores
- **Ação:** Remover cada valor sequencialmente
- **Asserção:** `Valores.Count() == 0` após remover todos

### 5.4 Testes de Alteração de IdadeMax

#### Teste 5.4.1: AlterarIdadeMaxValida
- **Objetivo:** Verificar que IdadeMax pode ser alterada
- **Arranjo:** PrecoInscricao criada
- **Ação:** `precoInscricao.IdadeMax = new InteiroPositivo(25)`
- **Asserção:** `IdadeMax.Valor == 25`

#### Teste 5.4.2: AtribuirIdadeMaxNulaDeveLancarExcecao
- **Objetivo:** Validar que IdadeMax não pode ser definida como null
- **Ação:** Tentar `IdadeMax = null`
- **Asserção:** ArgumentNullException deve ser lançada

---

## 6. PrecoInscricaoValor

**Localização:** `EventoWeb.Comum\Negocio\Entidades\PrecoInscricaoValor.cs`

**Classe Base:** `Entidade`

**Responsabilidade:** Armazenar o valor de uma inscrição para uma combinação específica de faixa etária e forma de pagamento.

### 6.1 Testes de Criação

#### Teste 6.1.1: CriarPrecoInscricaoValorComDadosValidos
- **Objetivo:** Criar PrecoInscricaoValor com sucesso
- **Arranjo:**
  - PrecoInscricao válida
  - FormaPagamento válida
  - Valor: ValorMonetario(200.00)
- **Ação:** Criar novo PrecoInscricaoValor
- **Asserção:**
  - PrecoInscricaoValor criado com sucesso
  - `Preco` definido
  - `Forma` definida
  - `Valor.Valor == 200.00`

#### Teste 6.1.2: CriarPrecoInscricaoValorComPrecoNulo
- **Objetivo:** Validar que Preco não pode ser nulo
- **Ação:** Tentar criar PrecoInscricaoValor com Preco = null
- **Asserção:** Exception deve ser lançada

#### Teste 6.1.3: CriarPrecoInscricaoValorComFormaNula
- **Objetivo:** Validar que FormaPagamento não pode ser nula
- **Ação:** Tentar criar PrecoInscricaoValor com Forma = null
- **Asserção:** Exception deve ser lançada

#### Teste 6.1.4: CriarPrecoInscricaoValorComValorNulo
- **Objetivo:** Validar que Valor não pode ser nulo
- **Ação:** Tentar criar PrecoInscricaoValor com Valor = null
- **Asserção:** ArgumentNullException deve ser lançada

### 6.2 Testes de Alteração de Valor

#### Teste 6.2.1: AlterarValorMonetarioValido
- **Objetivo:** Verificar que Valor pode ser alterado
- **Arranjo:** PrecoInscricaoValor criado
- **Ação:** `precoInscricaoValor.Valor = new ValorMonetario(250.00)`
- **Asserção:** `Valor.Valor == 250.00`

#### Teste 6.2.2: AtribuirValorNuloDeveLancarExcecao
- **Objetivo:** Validar que Valor não pode ser definido como null
- **Ação:** Tentar `Valor = null`
- **Asserção:** ArgumentNullException deve ser lançada

### 6.3 Testes de Propriedades Somente-Leitura

#### Teste 6.3.1: PrecoEhSomenteLeitura
- **Objetivo:** Verificar que Preco não tem setter público
- **Asserção:** Propriedade não possui setter público

#### Teste 6.3.2: FormaEhSomenteLeitura
- **Objetivo:** Verificar que Forma não tem setter público
- **Asserção:** Propriedade não possui setter público

---

## 7. ArquivoBinario

**Localização:** `EventoWeb.Comum\Negocio\Entidades\ArquivoBinario.cs`

**Classe Base:** `Entidade`

**Responsabilidade:** Armazenar arquivo binário com tipo específico.

### 7.1 Testes de Criação

#### Teste 7.1.1: CriarArquivoBinarioComDadosValidos
- **Objetivo:** Criar ArquivoBinario com sucesso
- **Arranjo:**
  - Arquivo: byte[] com 100 bytes
  - Tipo: EnumTipoArquivoBinario.PDF
- **Ação:** Criar novo ArquivoBinario
- **Asserção:**
  - ArquivoBinario criado
  - `Arquivo.Length == 100`
  - `Tipo == EnumTipoArquivoBinario.PDF`

#### Teste 7.1.2: CriarArquivoBinarioComTipoImagemPNG
- **Objetivo:** Criar ArquivoBinario com tipo PNG
- **Arranjo:** Tipo = EnumTipoArquivoBinario.ImagemPNG
- **Ação:** Criar ArquivoBinario
- **Asserção:** `Tipo == EnumTipoArquivoBinario.ImagemPNG`

#### Teste 7.1.3: CriarArquivoBinarioComTipoImagemJPEG
- **Objetivo:** Criar ArquivoBinario com tipo JPEG
- **Arranjo:** Tipo = EnumTipoArquivoBinario.ImagemJPEG
- **Ação:** Criar ArquivoBinario
- **Asserção:** `Tipo == EnumTipoArquivoBinario.ImagemJPEG`

### 7.2 Testes de Validações

#### Teste 7.2.1: CriarArquivoBinarioComArquivoNuloDeveLancarExcecao
- **Objetivo:** Validar que arquivo não pode ser nulo
- **Ação:** Tentar criar ArquivoBinario com Arquivo = null
- **Asserção:** ArgumentException deve ser lançada

#### Teste 7.2.2: CriarArquivoBinarioComArquivoVazioDeveLancarExcecao
- **Objetivo:** Validar que arquivo não pode estar vazio
- **Ação:** Tentar criar ArquivoBinario com Arquivo = byte[0]
- **Asserção:** ArgumentException deve ser lançada com mensagem "Arquivo vazio"

### 7.3 Testes de Setters

#### Teste 7.3.1: AlterarArquivoValido
- **Objetivo:** Verificar que Arquivo pode ser alterado
- **Arranjo:** ArquivoBinario criado
- **Ação:** `arquivoBinario.Arquivo = new byte[50]`
- **Asserção:** `Arquivo.Length == 50`

#### Teste 7.3.2: AtribuirArquivoNuloDeveLancarExcecao
- **Objetivo:** Validar que Arquivo não pode ser definido como null
- **Ação:** Tentar `Arquivo = null`
- **Asserção:** ArgumentException deve ser lançada

#### Teste 7.3.3: AtribuirArquivoVazioDeveLancarExcecao
- **Objetivo:** Validar que Arquivo não pode ser vazio
- **Ação:** Tentar `Arquivo = byte[0]`
- **Asserção:** ArgumentException deve ser lançada

#### Teste 7.3.4: AlterarTipoValido
- **Objetivo:** Verificar que Tipo pode ser alterado
- **Arranjo:** ArquivoBinario criado
- **Ação:** `arquivoBinario.Tipo = EnumTipoArquivoBinario.ImagemJPEG`
- **Asserção:** `Tipo == EnumTipoArquivoBinario.ImagemJPEG`

---

## 8. Conta (Financeiro)

**Localização:** `EventoWeb.Comum\Negocio\Entidades\Financeiro\Conta.cs`

**Classe Base:** `Entidade`

**Responsabilidade:** Gerenciar contas financeiras (receitas/despesas) com transações.

### 8.1 Testes de Criação

#### Teste 8.1.1: CriarContaComDadosValidos
- **Objetivo:** Criar Conta com sucesso
- **Arranjo:**
  - Pessoa: pessoa válida
  - Tipo: EnumTipoTransacao.Receita
  - Valor: ValorMonetario(500.00)
  - DataVencimento: DateTime.Now.AddDays(30)
- **Ação:** Criar nova Conta
- **Asserção:**
  - Conta criada
  - `Pessoa` definida
  - `Tipo == EnumTipoTransacao.Receita`
  - `Valor.Valor == 500.00`
  - `Liquidado == false`
  - `DataCriado == DateTime.Now` (mesmo dia)
  - `ValorTotalTransacoes.Valor == 0`
  - `ValorTotalDesconto.Valor == 0`
  - `ValorTotalJuros.Valor == 0`
  - `ValorTotalMulta.Valor == 0`
  - `Transacoes.Count() == 0`

#### Teste 8.1.2: CriarContaComPessoaNula
- **Objetivo:** Validar que Pessoa não pode ser nula
- **Ação:** Tentar criar Conta com Pessoa = null
- **Asserção:** Exception deve ser lançada

#### Teste 8.1.3: CriarContaComValorNulo
- **Objetivo:** Validar que Valor não pode ser nulo
- **Ação:** Tentar criar Conta com Valor = null
- **Asserção:** Exception deve ser lançada

### 8.2 Testes de Adição de Transações

#### Teste 8.2.1: AdicionarTransacaoComValorMenorQueTotalConta
- **Objetivo:** Adicionar transação sem liquidar conta
- **Arranjo:**
  - Conta com valor 500
  - ContaBancaria válida
  - Transacao com valor 200
- **Ação:** `conta.AdicionarTransacao(contaBancaria, DateTime.Now, valorTransacao)`
- **Asserção:**
  - Transacao adicionada
  - `Transacoes.Count() == 1`
  - `ValorTotalTransacoes.Valor == 200`
  - `Liquidado == false`

#### Teste 8.2.2: AdicionarTransacaoQueAtingeValorTotalDaConta
- **Objetivo:** Adicionar transação que liquida a conta
- **Arranjo:**
  - Conta com valor 500
  - Transacao com valor 500
- **Ação:** `conta.AdicionarTransacao(..., 500)`
- **Asserção:**
  - `Liquidado == true`
  - `ValorTotalTransacoes.Valor == 500`

#### Teste 8.2.3: AdicionarTransacaoSuperandoValorTotalDaConta
- **Objetivo:** Adicionar transação que supera o total deve liquidar
- **Arranjo:**
  - Conta com valor 500
  - Transacao com valor 300
  - Segunda transacao com valor 300
- **Ação:** Adicionar segunda transacao
- **Asserção:**
  - `Liquidado == true`
  - `ValorTotalTransacoes.Valor == 600`

#### Teste 8.2.4: AdicionarTransacaoComDescontoJurosMulta
- **Objetivo:** Adicionar transação com descontos, juros e multa
- **Arranjo:**
  - ValorTransacao: 200
  - Desconto: 50
  - Juros: 10
  - Multa: 5
- **Ação:** `conta.AdicionarTransacao(..., 200, multa: 5, juros: 10, desconto: 50)`
- **Asserção:**
  - `ValorTotalTransacoes.Valor == 200`
  - `ValorTotalDesconto.Valor == 50`
  - `ValorTotalJuros.Valor == 10`
  - `ValorTotalMulta.Valor == 5`

#### Teste 8.2.5: AdicionarTransacaoEmContaLiquidadaDeveLancarExcecao
- **Objetivo:** Validar que não pode adicionar transação em conta liquidada
- **Arranjo:**
  - Conta liquidada
- **Ação:** Tentar adicionar transacao
- **Asserção:** Exception deve ser lançada com mensagem apropriada

### 8.3 Testes de Remoção de Transações

#### Teste 8.3.1: RemoverTransacaoExistente
- **Objetivo:** Remover transação da conta
- **Arranjo:**
  - Conta com 2 transações
- **Ação:** `conta.RemoverTransacao(transacao1)`
- **Asserção:**
  - `Transacoes.Count() == 1`
  - Valores totais recalculados

#### Teste 8.3.2: RemoverTransacaoInexistenteDeveLancarExcecao
- **Objetivo:** Validar que remoção de transação inexistente falha
- **Arranjo:**
  - Conta com transações
  - TransacaoConta não pertencente à conta
- **Ação:** Tentar remover transação inexistente
- **Asserção:** Exception deve ser lançada

#### Teste 8.3.3: RemoverTransacaoEmContaLiquidadaDeveLancarExcecao
- **Objetivo:** Validar que não pode remover transação de conta liquidada
- **Arranjo:**
  - Conta liquidada com transações
- **Ação:** Tentar remover transacao
- **Asserção:** Exception deve ser lançada

#### Teste 8.3.4: RemoverTransacaoRecalculaTotais
- **Objetivo:** Verificar que totais são recalculados ao remover
- **Arranjo:**
  - Conta com 2 transações (200 e 300)
  - Total antes: 500
- **Ação:** Remover segunda transacao
- **Asserção:**
  - `ValorTotalTransacoes.Valor == 200`

### 8.4 Testes de Reabertura de Conta

#### Teste 8.4.1: ReiniciarContaLiquidada
- **Objetivo:** Reabrir conta liquidada
- **Arranjo:**
  - Conta liquidada com transações
- **Ação:** `conta.Reabrir()`
- **Asserção:**
  - `Liquidado == false`
  - `Transacoes.Count() == 0`
  - Todos os totais zerados

#### Teste 8.4.2: ReiniciarContaNaoLiquidadaDeveLancarExcecao
- **Objetivo:** Validar que não pode reabrir conta não liquidada
- **Arranjo:**
  - Conta não liquidada
- **Ação:** Tentar `Reabrir()`
- **Asserção:** Exception deve ser lançada

### 8.5 Testes de Setters de Propriedades

#### Teste 8.5.1: AlterarValorEmContaNaoLiquidada
- **Objetivo:** Alterar valor em conta aberta
- **Arranjo:** Conta não liquidada
- **Ação:** `conta.Valor = new ValorMonetario(1000)`
- **Asserção:** `Valor.Valor == 1000`

#### Teste 8.5.2: AlterarValorEmContaLiquidadaDeveLancarExcecao
- **Objetivo:** Validar que não pode alterar valor de conta liquidada
- **Ação:** Tentar alterar valor em conta liquidada
- **Asserção:** Exception deve ser lançada

#### Teste 8.5.3: AlterarLiquidadoEmContaNaoLiquidada
- **Objetivo:** Marcar conta como liquidada
- **Ação:** `conta.Liquidado = true`
- **Asserção:** Exception deve ser lançada (não pode ser feito via setter)

#### Teste 8.5.4: AlterarDescricao
- **Objetivo:** Alterar descrição em conta aberta
- **Ação:** `conta.Descricao = new StringClob("Nova descrição")`
- **Asserção:** Descrição alterada

#### Teste 8.5.5: AlterarDataVencimento
- **Objetivo:** Alterar data de vencimento em conta aberta
- **Ação:** `conta.DataVencimento = DateTime.Now.AddDays(60)`
- **Asserção:** Data alterada

#### Teste 8.5.6: AlterarTipo
- **Objetivo:** Alterar tipo em conta aberta
- **Ação:** `conta.Tipo = EnumTipoTransacao.Despesa`
- **Asserção:** Tipo alterado

---

## 9. ContaBancaria

**Localização:** `EventoWeb.Comum\Negocio\Entidades\Financeiro\ContaBancaria.cs`

**Classe Base:** `Entidade`

**Responsabilidade:** Representar uma conta bancária.

### 9.1 Testes de Criação

#### Teste 9.1.1: CriarContaBancariaComDadosValidos
- **Objetivo:** Criar ContaBancaria com sucesso
- **Arranjo:** NomeConta = "Banco Brasil - Corrente"
- **Ação:** Criar nova ContaBancaria
- **Asserção:**
  - ContaBancaria criada
  - `NomeConta.Valor == "Banco Brasil - Corrente"`

#### Teste 9.1.2: CriarContaBancariaComNomeNulo
- **Objetivo:** Validar que NomeConta não pode ser nulo
- **Ação:** Tentar criar ContaBancaria com NomeConta = null
- **Asserção:** Exception deve ser lançada

### 9.2 Testes de Setters

#### Teste 9.2.1: AlterarNomeContaValido
- **Objetivo:** Alterar nome da conta
- **Arranjo:** ContaBancaria criada
- **Ação:** `contaBancaria.NomeConta = new String200("Novo Nome")`
- **Asserção:** Nome alterado

#### Teste 9.2.2: AtribuirNomeNuloDeveLancarExcecao
- **Objetivo:** Validar que NomeConta não pode ser null
- **Ação:** Tentar `NomeConta = null`
- **Asserção:** Exception deve ser lançada

---

## 10. FormaPagamento

**Localização:** `EventoWeb.Comum\Negocio\Entidades\Financeiro\FormaPagamento.cs`

**Classe Base:** `Entidade`

**Responsabilidade:** Definir formas de pagamento com suporte a parcelamento.

### 10.1 Testes de Criação

#### Teste 10.1.1: CriarFormaPagamentoComDadosValidos
- **Objetivo:** Criar FormaPagamento com sucesso
- **Arranjo:**
  - Nome: "Cartão de Crédito"
  - Tipo: EnumTipoPagamento.CartaoCredito
- **Ação:** Criar nova FormaPagamento
- **Asserção:**
  - FormaPagamento criada
  - `Nome.Valor == "Cartão de Crédito"`
  - `Tipo == EnumTipoPagamento.CartaoCredito`
  - `Parcelas.Minimo == 1`
  - `Parcelas.Maximo == 1`

#### Teste 10.1.2: CriarFormaPagamentoComNomeNulo
- **Objetivo:** Validar que Nome não pode ser nulo
- **Ação:** Tentar criar FormaPagamento com Nome = null
- **Asserção:** ArgumentNullException deve ser lançada

### 10.2 Testes de Setters

#### Teste 10.2.1: AlterarNomeValido
- **Objetivo:** Alterar nome da forma de pagamento
- **Arranjo:** FormaPagamento criada
- **Ação:** `formaPagamento.Nome = new String200("PIX")`
- **Asserção:** Nome alterado

#### Teste 10.2.2: AtribuirNomeNuloDeveLancarExcecao
- **Objetivo:** Validar que Nome não pode ser null
- **Ação:** Tentar `Nome = null`
- **Asserção:** ArgumentNullException deve ser lançada

#### Teste 10.2.3: AlterarTipoPagemento
- **Objetivo:** Alterar tipo de pagamento
- **Arranjo:** FormaPagamento criada
- **Ação:** `formaPagamento.Tipo = EnumTipoPagamento.Boleto`
- **Asserção:** Tipo alterado

### 10.3 Testes de Definição de Parcelamento

#### Teste 10.3.1: DefinirParcelasValidas
- **Objetivo:** Definir parcelamento válido
- **Arranjo:** FormaPagamento com tipo suportando parcelamento
- **Ação:** `formaPagamento.DefinirParcelas(new IntervaloInteiroPositivo(1, 12))`
- **Asserção:**
  - `Parcelas.Minimo == 1`
  - `Parcelas.Maximo == 12`

#### Teste 10.3.2: DefinirParcelasNulaDeveLancarExcecao
- **Objetivo:** Validar que Parcelas não pode ser null
- **Ação:** Tentar `DefinirParcelas(null)`
- **Asserção:** ArgumentNullException deve ser lançada

---

## 11. Transacao

**Localização:** `EventoWeb.Comum\Negocio\Entidades\Financeiro\Transacao.cs`

**Classe Base:** `Entidade`

**Responsabilidade:** Registrar transações bancárias individuais.

### 11.1 Testes de Criação

#### Teste 11.1.1: CriarTransacaoComDadosValidos
- **Objetivo:** Criar Transacao com sucesso
- **Arranjo:**
  - Tipo: EnumTipoTransacao.Receita
  - ContaBancaria: válida
  - DataHora: DateTime.Now
  - Valor: ValorMonetario(300.00)
  - Descricao: "Pagamento recebido"
- **Ação:** Criar nova Transacao
- **Asserção:**
  - Transacao criada
  - `Tipo == EnumTipoTransacao.Receita`
  - `ContaBancaria` definida
  - `DataHora` definida
  - `Valor.Valor == 300.00`
  - `Descricao.Valor == "Pagamento recebido"`

#### Teste 11.1.2: CriarTransacaoComContaBancariaNula
- **Objetivo:** Validar que ContaBancaria não pode ser nula
- **Acao:** Tentar criar Transacao com ContaBancaria = null
- **Asserção:** Exception deve ser lançada

#### Teste 11.1.3: CriarTransacaoComValorNulo
- **Objetivo:** Validar que Valor não pode ser nulo
- **Ação:** Tentar criar Transacao com Valor = null
- **Asserção:** Exception deve ser lançada

### 11.2 Testes de Somente-Leitura

#### Teste 11.2.1: PropriedadesEhSomenteLeitura
- **Objetivo:** Verificar que propriedades não podem ser alteradas
- **Asserção:**
  - `ContaBancaria` é somente-leitura
  - `DataHora` é somente-leitura
  - `Descricao` é somente-leitura
  - `Valor` é somente-leitura
  - `Tipo` é somente-leitura

---

## 12. TransacaoConta

**Localização:** `EventoWeb.Comum\Negocio\Entidades\Financeiro\TransacaoConta.cs`

**Classe Base:** `Entidade`

**Responsabilidade:** Vincular transações bancárias a contas com adicionais (juros, multas, descontos).

### 12.1 Testes de Criação

#### Teste 12.1.1: CriarTransacaoContaComDadosValidos
- **Objetivo:** Criar TransacaoConta com sucesso
- **Arranjo:**
  - ContaBancaria: válida
  - Conta: válida
  - Data: DateTime.Now
  - ValorTransacao: ValorMonetario(400.00)
  - Multa: null (usa padrão 0)
  - Juros: null (usa padrão 0)
  - Desconto: null (usa padrão 0)
- **Ação:** Criar nova TransacaoConta
- **Asserção:**
  - TransacaoConta criada
  - `Conta` definida
  - `Data` definida
  - `ValorTransacao.Valor == 400.00`
  - `Multa.Valor == 0`
  - `Juros.Valor == 0`
  - `Desconto.Valor == 0`
  - `Transacao` não é null (criada automaticamente)

#### Teste 12.1.2: CriarTransacaoContaComContaNula
- **Objetivo:** Validar que Conta não pode ser nula
- **Ação:** Tentar criar TransacaoConta com Conta = null
- **Asserção:** Exception deve ser lançada

#### Teste 12.1.3: CriarTransacaoContaComValorNulo
- **Objetivo:** Validar que ValorTransacao não pode ser nulo
- **Ação:** Tentar criar TransacaoConta com ValorTransacao = null
- **Asserção:** Exception deve ser lançada

### 12.2 Testes com Valores Adicionais

#### Teste 12.2.1: CriarTransacaoContaComMulta
- **Objetivo:** Criar transacao com multa
- **Arranjo:** Multa = ValorMonetario(50.00)
- **Ação:** Criar TransacaoConta
- **Asserção:** `Multa.Valor == 50.00`

#### Teste 12.2.2: CriarTransacaoContaComJuros
- **Objetivo:** Criar transacao com juros
- **Arranjo:** Juros = ValorMonetario(25.00)
- **Ação:** Criar TransacaoConta
- **Asserção:** `Juros.Valor == 25.00`

#### Teste 12.2.3: CriarTransacaoContaComDesconto
- **Objetivo:** Criar transacao com desconto
- **Arranjo:** Desconto = ValorMonetario(100.00)
- **Ação:** Criar TransacaoConta
- **Asserção:** `Desconto.Valor == 100.00`

#### Teste 12.2.4: CriarTransacaoContaComTodosOsValoresAdicionais
- **Objetivo:** Criar transacao com todos os valores adicionais
- **Arranjo:**
  - ValorTransacao: 400.00
  - Multa: 50.00
  - Juros: 25.00
  - Desconto: 100.00
- **Ação:** Criar TransacaoConta
- **Asserção:** Todos os valores definidos corretamente

#### Teste 12.2.5: CriarTransacaoContaComValorZero_TransacaoNaoEhCriada
- **Objetivo:** Verificar que Transacao não é criada se ValorTransacao <= 0
- **Arranjo:** ValorTransacao = 0
- **Ação:** Criar TransacaoConta
- **Asserção:** `Transacao == null`

### 12.3 Testes de Somente-Leitura

#### Teste 12.3.1: PropriedadesEhSomenteLeitura
- **Objetivo:** Verificar que propriedades não podem ser alteradas
- **Asserção:**
  - `Transacao` é somente-leitura
  - `Conta` é somente-leitura
  - `Data` é somente-leitura
  - `ValorTransacao` é somente-leitura
  - `Multa` é somente-leitura
  - `Juros` é somente-leitura
  - `Desconto` é somente-leitura

---

## Resumo de Cobertura

| Classe | Total de Testes | Categorias |
|--------|-----------------|-----------|
| Pessoa | 17 | Criação, Validações, Setters, Opcionais, Nullabilidade, Igualdade |
| Evento | 16 | Criação, Validações, Setters, Opcionais, Data |
| InscricaoParticipante | 15 | Criação, Validações, Propriedades, Estados |
| InscricaoInfantil | 13 | Criação, Validações, Aceitação, Responsáveis |
| Pedido | 9 | Criação, Validações, Múltiplas Inscrições, Conta |
| PrecoInscricao | 9 | Criação, Validações, Valores, IdadeMax |
| PrecoInscricaoValor | 7 | Criação, Validações, Alterações, Read-only |
| ArquivoBinario | 10 | Criação, Validações, Setters |
| Conta | 18 | Criação, Transações, Remoção, Reabertura, Setters |
| ContaBancaria | 4 | Criação, Setters |
| FormaPagamento | 9 | Criação, Setters, Parcelamento |
| Transacao | 4 | Criação, Validações, Read-only |
| TransacaoConta | 11 | Criação, Valores Adicionais, Read-only |
| **TOTAL** | **152 TESTES** | |

---

## Notas Importantes

1. **Ordem de Execução:** Alguns testes podem depender da criação bem-sucedida de entidades relacionadas. Considere usar `Fixtures` ou `Factories` para criar dados de teste reutilizáveis.

2. **Objetos de Valor:** Os testes usam objetos de valor (CPF, Email, Telefone, etc.). Certifique-se de que eles estão com testes já cobrindo sua lógica de validação.

3. **Máquina de Estados:** Especialmente para Inscricao, os testes de transição de estado são críticos. Certifique-se de testar todas as transições válidas e inválidas.

4. **Conta Financeira:** Os testes de Conta são complexos. Considere usar fixtures para criar múltiplos cenários de transações.

5. **Padrão de Nomeação:** Todos os testes seguem o padrão `NomeClasse_Acao_Resultado` para melhor legibilidade.

6. **Exceções:** Use `Assert.Throws<TipoExcecao>()` para validar que exceções esperadas são lançadas.

---

**Próximas Etapas:** Este plano será usado por um agente para criar os testes automaticamente. Após a criação, verifique a cobertura com ferramentas de cobertura de código.
