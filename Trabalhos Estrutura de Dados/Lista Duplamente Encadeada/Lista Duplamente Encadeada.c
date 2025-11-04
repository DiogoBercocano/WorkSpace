#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct Livro {
    int id;
    char titulo[100];
    char autor[100];
    struct Livro *anterior;
    struct Livro *proximo;
} Livro;

Livro *inicio = NULL;
Livro *fim = NULL;

// Função para contar elementos da lista
int contarElementos() {
    int count = 0;
    Livro *p = inicio;
    while (p != NULL) {
        count++;
        p = p->proximo;
    }
    return count;
}

// Inserir cadastro de forma ordenada pelo id
void inserirLivro(int id, char *titulo, char *autor) {
    Livro *novo = (Livro*)malloc(sizeof(Livro));
    if (!novo) {
        printf("Erro de alocacao\n");
        return;
    }
    novo->id = id;
    strncpy(novo->titulo, titulo, 100);
    strncpy(novo->autor, autor, 100);
    novo->anterior = NULL;
    novo->proximo = NULL;

    if (inicio == NULL) { // lista vazia
        inicio = fim = novo;
        return;
    }

    Livro *p = inicio;
    while (p != NULL && p->id < id) {
        p = p->proximo;
    }
    if (p == inicio) { // Inserir no começo
        novo->proximo = inicio;
        inicio->anterior = novo;
        inicio = novo;
    } else if (p == NULL) { // Inserir no fim
        fim->proximo = novo;
        novo->anterior = fim;
        fim = novo;
    } else { // Inserir no meio
        novo->proximo = p;
        novo->anterior = p->anterior;
        p->anterior->proximo = novo;
        p->anterior = novo;
    }
}

// Remover cadastro pelo ID
void removerLivro(int id) {
    Livro *p = inicio;
    while (p != NULL && p->id != id) {
        p = p->proximo;
    }
    if (p == NULL) {
        printf("Livro com ID %d nao encontrado.\n", id);
        return;
    }
    if (p == inicio && p == fim) { // unico elemento
        inicio = fim = NULL;
    } else if (p == inicio) { // primeiro elemento
        inicio = p->proximo;
        inicio->anterior = NULL;
    } else if (p == fim) { // ultimo elemento
        fim = p->anterior;
        fim->proximo = NULL;
    } else {
        p->anterior->proximo = p->proximo;
        p->proximo->anterior = p->anterior;
    }
    free(p);
    printf("Livro removido com sucesso.\n");
}

// Buscar por qualquer campo (id, titulo ou autor)
void buscarLivros(char *campo, char *valor) {
    Livro *p = inicio;
    int encontrado = 0;
    while (p != NULL) {
        if ((strcmp(campo, "id") == 0 && atoi(valor) == p->id) ||
            (strcmp(campo, "titulo") == 0 && strcasecmp(valor, p->titulo) == 0) ||
            (strcmp(campo, "autor") == 0 && strcasecmp(valor, p->autor) == 0)) {
            printf("ID: %d | Titulo: %s | Autor: %s\n", p->id, p->titulo, p->autor);
            encontrado = 1;
        }
        p = p->proximo;
    }
    if (!encontrado) {
        printf("Nenhum livro encontrado para %s = %s\n", campo, valor);
    }
}

// Listar todos os cadastros em ordem crescente de ID
void listarParaFrente() {
    Livro *p = inicio;
    if (p == NULL) {
        printf("Lista vazia.\n");
        return;
    }
    printf("Lista crescente de livros:\n");
    while (p != NULL) {
        printf("ID: %d | Titulo: %s | Autor: %s\n", p->id, p->titulo, p->autor);
        p = p->proximo;
    }
}

// Listar todos os cadastros em ordem decrescente de ID
void listarParaTras() {
    Livro *p = fim;
    if (p == NULL) {
        printf("Lista vazia.\n");
        return;
    }
    printf("Lista decrescente de livros:\n");
    while (p != NULL) {
        printf("ID: %d | Titulo: %s | Autor: %s\n", p->id, p->titulo, p->autor);
        p = p->anterior;
    }
}

// Editar informações de um cadastro existente
void editarLivro(int id) {
    Livro *p = inicio;
    while (p != NULL && p->id != id) {
        p = p->proximo;
    }
    if (p == NULL) {
        printf("Livro com ID %d nao encontrado.\n", id);
        return;
    }
    printf("Editando livro (ID: %d)\n", id);
    printf("Titulo atual: %s\n", p->titulo);
    printf("Novo titulo: ");
    fgets(p->titulo, 100, stdin);
    p->titulo[strcspn(p->titulo, "\n")] = 0; // remove '\n'

    printf("Autor atual: %s\n", p->autor);
    printf("Novo autor: ");
    fgets(p->autor, 100, stdin);
    p->autor[strcspn(p->autor, "\n")] = 0;
    printf("Livro editado com sucesso.\n");
}

// Salvar lista em arquivo
void salvarArquivo() {
    FILE *arq = fopen("livros.dat", "wb");
    if (!arq) {
        printf("Erro ao abrir arquivo para salvar.\n");
        return;
    }
    Livro *p = inicio;
    while (p != NULL) {
        fwrite(p, sizeof(Livro), 1, arq);
        p = p->proximo;
    }
    fclose(arq);
    printf("Lista salva no arquivo com sucesso.\n");
}

// Carregar lista de arquivo
void carregarArquivo() {
    FILE *arq = fopen("livros.dat", "rb");
    if (!arq) {
        printf("Arquivo nao encontrado, iniciando lista vazia.\n");
        return;
    }
    printf("Carregando lista do arquivo...\n");
    Livro temp;
    while (fread(&temp, sizeof(Livro), 1, arq) == 1) {
        inserirLivro(temp.id, temp.titulo, temp.autor);
    }
    fclose(arq);
    printf("Lista carregada com sucesso.\n");
}

// Função para limpar a lista e liberar memória
void limparLista() {
    Livro *p = inicio;
    while (p != NULL) {
        Livro *temp = p;
        p = p->proximo;
        free(temp);
    }
    inicio = fim = NULL;
}

// Menu principal
void menu() {
    int opcao, id;
    char titulo[100], autor[100], campo[20], valor[100];
    char c;

    carregarArquivo();

    do {
        printf("\n=== Menu Cadastro de Livros ===\n");
        printf("1. Inserir livro\n");
        printf("2. Remover livro pelo ID\n");
        printf("3. Buscar livro\n");
        printf("4. Listar livros para frente\n");
        printf("5. Listar livros para tras\n");
        printf("6. Editar livro\n");
        printf("7. Quantidade de livros\n");
        printf("0. Sair\n");
        printf("Escolha a opcao: ");
        scanf("%d", &opcao);
        while ((c = getchar()) != '\n' && c != EOF);

        switch (opcao) {
            case 1:
                printf("ID: ");
                scanf("%d", &id);
                while ((c = getchar()) != '\n' && c != EOF);
                printf("Titulo: ");
                fgets(titulo, 100, stdin);
                titulo[strcspn(titulo, "\n")] = 0;
                printf("Autor: ");
                fgets(autor, 100, stdin);
                autor[strcspn(autor, "\n")] = 0;
                inserirLivro(id, titulo, autor);
                printf("Livro inserido com sucesso.\n");
                break;
            case 2:
                printf("ID para remover: ");
                scanf("%d", &id);
                while ((c = getchar()) != '\n' && c != EOF);
                removerLivro(id);
                break;
            case 3:
                printf("Campo para buscar (id, titulo, autor): ");
                fgets(campo, 20, stdin);
                campo[strcspn(campo, "\n")] = 0;
                printf("Valor para buscar: ");
                fgets(valor, 100, stdin);
                valor[strcspn(valor, "\n")] = 0;
                buscarLivros(campo, valor);
                break;
            case 4:
                listarParaFrente();
                break;
            case 5:
                listarParaTras();
                break;
            case 6:
                printf("ID para editar: ");
                scanf("%d", &id);
                while ((c = getchar()) != '\n' && c != EOF);
                editarLivro(id);
                break;
            case 7:
                printf("Quantidade de livros: %d\n", contarElementos());
                break;
            case 0:
                printf("Saindo...\n");
                salvarArquivo();
                limparLista();
                break;
            default:
                printf("Opcao invalida.\n");
        }
    } while (opcao != 0);
}

int main() {
    menu();
    return 0;
}

