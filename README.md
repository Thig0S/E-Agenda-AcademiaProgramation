# 📋 Especificação do Projeto — Sistema de Gestão

Este projeto consiste em um sistema integrado para gerenciamento de contatos, compromissos, tarefas e controle financeiro pessoal (categorias e despesas). Abaixo estão detalhados os requisitos funcionais e as regras de negócio para cada módulo.

---

## 👥 1. Módulo de Contatos

### Requisitos Funcionais
- [x] Inserir novos contatos
- [x] Editar contatos existentes
- [x] Excluir contatos cadastrados
- [x] Visualizar contatos cadastrados

### Regras de Negócio
| Campo | Tipo / Limitação | Obrigatoriedade |
| :--- | :--- | :--- |
| **Nome** | Texto (2 a 100 caracteres) | Obrigatório |
| **Email** | Formato válido e único | Obrigatório |
| **Telefone** | `(XX) XXXX-XXXX` ou `(XX) XXXXX-XXXX` (Único) | Obrigatório |
| **Cargo** | Texto | Opcional |
| **Empresa** | Texto | Opcional |

> ⚠️ **Restrições:**
> - Não é permitido o cadastro de contatos que compartilhem o mesmo e-mail e/ou telefone.
> - Não é permitida a exclusão de um contato que possua compromissos vinculados.

---

## 📅 2. Módulo de Compromissos

### Requisitos Funcionais
- [x] Inserir novos compromissos
- [x] Editar compromissos existentes
- [x] Excluir compromissos cadastrados
- [x] Visualizar compromissos cadastrados

### Regras de Negócio
| Campo | Tipo / Limitação | Obrigatoriedade |
| :--- | :--- | :--- |
| **Assunto** | Texto (2 a 100 caracteres) | Obrigatório |
| **Data de Ocorrência** | Data válida | Obrigatório |
| **Hora de Início** | Horário válido | Obrigatório |
| **Hora de Término** | Horário válido | Obrigatório |
| **Tipo** | `Remoto` ou `Presencial` | Obrigatório |
| **Local** | Texto (Obrigatório se *Presencial*) | Condicional |
| **Link** | URL válida (Obrigatório se *Remoto*) | Condicional |
| **Contato** | Vínculo com Módulo de Contatos | Opcional |

> ⚠️ **Restrições:**
> - O sistema deve validar e impedir qualquer conflito de horários entre compromissos cadastrados.

---

## 🗂️ 3. Módulo de Categorias

### Requisitos Funcionais
- [x] Cadastrar novas categorias
- [x] Editar categorias existentes
- [x] Excluir categorias
- [x] Visualizar todas as categorias
- [x] Visualizar despesas filtradas por uma categoria específica

### Regras de Negócio
- **Campos Obrigatórios:** `Título` (Texto, 2 a 100 caracteres, único).
- **Despesas:** São vinculadas posteriormente ao cadastro da categoria.
- > ⚠️ **Restrição:** Não é permitida a exclusão de categorias que possuam despesas relacionadas.

---

## 💰 4. Módulo de Despesas

### Requisitos Funcionais
- [x] Cadastrar novas despesas
- [x] Editar despesas existentes
- [x] Excluir despesas
- [x] Visualizar todas as despesas

### Regras de Negócio
| Campo | Tipo / Limitação | Obrigatoriedade |
| :--- | :--- | :--- |
| **Descrição** | Texto (2 a 100 caracteres) | Obrigatório |
| **Valor** | Monetário ($ R$) | Obrigatório |
| **Forma de Pagamento** | `À Vista`, `Crédito` ou `Débito` | Obrigatório |
| **Categorias** | Vínculo com 1 ou mais Categorias | Obrigatório |
| **Data de Ocorrência**| Data (Padrão: data atual do cadastro) | Opcional |

---

## ⧉ 5. Módulo de Tarefas & Itens

### Requisitos Funcionais (Tarefas)
- [x] Cadastrar e editar tarefas
- [x] Excluir tarefas
- [x] Visualizar todas as tarefas (com filtros para *Pendentes* e *Concluídas*)
- [x] Visualizar tarefas agrupadas por prioridade

### Regras de Negócio (Tarefas)
- **Campos Obrigatórios:**
  - `Título` (2 a 100 caracteres)
  - `Prioridade` (`Baixa`, `Normal`, `Alta`)
  - `Data de Criação` e `Data de Conclusão`
  - `Status de Conclusão` e `Percentual Concluído` (%)
  - `Itens da Tarefa` (Opcional)

### 5.1 Submódulo: Itens de Tarefas
- **Funcionalidades:** Permitir adicionar, remover e marcar itens como concluídos.
- **Campos Obrigatórios:** `Título` (2-100 caracteres), `Status de Conclusão` e Vínculo com a `Tarefa`.
- > ⚙️ **Automação:** Ao concluir ou alterar o status de um item, o sistema deve recalcular automaticamente o percentual (`%`) de conclusão da tarefa pai.