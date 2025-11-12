#include <stdio.h>
#include <stdlib.h>

// Estrutura do nó da pilha
typedef struct No {
    int dado;
    struct No* proximo;
} No;

// Estrutura da pilha
typedef struct Pilha {
    No* topo;
} Pilha;

// Inicializa uma nova pilha
Pilha* criarPilha() {
    Pilha* p = (Pilha*) malloc(sizeof(Pilha));
    p->topo = NULL;
    return p;
}

// Empilha elemento no topo (Create)
void empilhar(Pilha* p, int dado) {
    No* novo = (No*) malloc(sizeof(No));
    novo->dado = dado;
    novo->proximo = p->topo;
    p->topo = novo;
}

// Desempilha elemento do topo (Delete)
int desempilhar(Pilha* p) {
    if (p->topo == NULL) return -1; // Pilha vazia
    No* temp = p->topo;
    int dado = temp->dado;
    p->topo = temp->proximo;
    free(temp);
    return dado;
}

// Imprime elementos da pilha (Read)
void imprimirPilha(Pilha* p) {
    No* atual = p->topo;
    printf("Pilha: ");
    while (atual != NULL) {
        printf("%d ", atual->dado);
        atual = atual->proximo;
    }
    printf("\n");
}

// Busca valor na pilha
No* buscarPilha(Pilha* p, int valor) {
    No* atual = p->topo;
    while (atual != NULL) {
        if (atual->dado == valor)
            return atual;
        atual = atual->proximo;
    }
    return NULL;
}

// Atualiza valor no topo ou qualquer nó (Update)
int atualizarPilha(Pilha* p, int valorAntigo, int valorNovo) {
    No* no = buscarPilha(p, valorAntigo);
    if (no != NULL) {
        no->dado = valorNovo;
        return 1;
    }
    return 0;
}

// Menu CRUD principal
int main() {
    Pilha* p = criarPilha();
    int op, valor, antigo, novo, removido;

    do {
        printf("\n--- MENU CRUD PILHA ---\n");
        printf("1. Empilhar (inserir)\n");
        printf("2. Listar pilha\n");
        printf("3. Buscar valor\n");
        printf("4. Atualizar valor\n");
        printf("5. Desempilhar (remover)\n");
        printf("0. Sair\nEscolha: ");
        scanf("%d", &op);

        switch (op) {
            case 1:
                printf("Valor para empilhar: ");
                scanf("%d", &valor);
                empilhar(p, valor);
                printf("Empilhado!\n");
                break;
            case 2:
                imprimirPilha(p);
                break;
            case 3:
                printf("Valor para buscar: ");
                scanf("%d", &valor);
                if (buscarPilha(p, valor))
                    printf("Valor %d está na pilha!\n", valor);
                else
                    printf("Valor %d NÃO está na pilha.\n", valor);
                break;
            case 4:
                printf("Valor antigo a atualizar: ");
                scanf("%d", &antigo);
                printf("Novo valor: ");
                scanf("%d", &novo);
                if (atualizarPilha(p, antigo, novo))
                    printf("Valor atualizado!\n");
                else
                    printf("Valor antigo NÃO encontrado.\n");
                break;
            case 5:
                removido = desempilhar(p);
                if (removido != -1)
                    printf("Removido do topo: %d\n", removido);
                else
                    printf("Pilha vazia!\n");
                break;
            case 0:
                printf("Finalizando...\n");
                break;
            default:
                printf("Opção inválida!\n");
        }
    } while (op != 0);

    return 0;
}

