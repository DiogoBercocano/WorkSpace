#include <stdio.h>
#include <stdlib.h>

// Estrutura do nó da fila
typedef struct No {
    int dado;
    struct No* proximo;
} No;

// Estrutura da fila com ponteiro para inicio e fim
typedef struct Fila {
    No *inicio, *fim;
} Fila;

// Inicializa uma nova fila
Fila* criarFila() {
    Fila* f = (Fila*) malloc(sizeof(Fila));
    f->inicio = f->fim = NULL;
    return f;
}

// Insere elemento no final da fila (Create)
void enfileirar(Fila* f, int dado) {
    No* novo = (No*) malloc(sizeof(No));
    novo->dado = dado;
    novo->proximo = NULL;
    if (f->fim == NULL) {
        f->inicio = f->fim = novo;
        return;
    }
    f->fim->proximo = novo;
    f->fim = novo;
}

// Remove elemento do início da fila (Delete)
int desenfileirar(Fila* f) {
    if (f->inicio == NULL) return -1; // Fila vazia
    No* temp = f->inicio;
    int dado = temp->dado;
    f->inicio = f->inicio->proximo;
    if (f->inicio == NULL) f->fim = NULL;
    free(temp);
    return dado;
}

// Imprime os elementos da fila (Read)
void imprimirFila(Fila* f) {
    No* atual = f->inicio;
    printf("Fila: ");
    while (atual != NULL) {
        printf("%d ", atual->dado);
        atual = atual->proximo;
    }
    printf("\n");
}

// Busca valor na fila
No* buscarFila(Fila* f, int valor) {
    No* atual = f->inicio;
    while (atual != NULL) {
        if (atual->dado == valor)
            return atual;
        atual = atual->proximo;
    }
    return NULL;
}

// Atualiza valor na fila (Update)
int atualizarFila(Fila* f, int valorAntigo, int valorNovo) {
    No* no = buscarFila(f, valorAntigo);
    if (no != NULL) {
        no->dado = valorNovo;
        return 1;
    }
    return 0;
}

// Função principal com menu CRUD
int main() {
    Fila* f = criarFila();
    int op, valor, antigo, novo, removido;
    do {
        printf("\n--- MENU CRUD FILA ---\n");
        printf("1. Inserir (Enfileirar)\n");
        printf("2. Listar fila\n");
        printf("3. Buscar valor\n");
        printf("4. Atualizar valor\n");
        printf("5. Remover (Desenfileirar)\n");
        printf("0. Sair\nEscolha: ");
        scanf("%d", &op);

        switch (op) {
            case 1:
                printf("Valor para inserir: ");
                scanf("%d", &valor);
                enfileirar(f, valor);
                printf("Inserido!\n");
                break;
            case 2:
                imprimirFila(f);
                break;
            case 3:
                printf("Valor para buscar: ");
                scanf("%d", &valor);
                if (buscarFila(f, valor))
                    printf("Valor %d encontrado na fila!\n", valor);
                else
                    printf("Valor %d NÃO encontrado.\n", valor);
                break;
            case 4:
                printf("Valor antigo: ");
                scanf("%d", &antigo);
                printf("Novo valor: ");
                scanf("%d", &novo);
                if (atualizarFila(f, antigo, novo))
                    printf("Valor atualizado!\n");
                else
                    printf("Valor antigo NÃO encontrado.\n");
                break;
            case 5:
                removido = desenfileirar(f);
                if (removido != -1)
                    printf("Removido do início: %d\n", removido);
                else
                    printf("Fila vazia!\n");
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

