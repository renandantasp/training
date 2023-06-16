#include <stdlib.h>
#include <stdio.h>

#define COUNT 10

typedef struct Tnode {
  int data;
  struct Tnode* left;
  struct Tnode* right;
} Tnode;

Tnode* create_node(int value);
Tnode* min_node(Tnode* node);
void insert_node(Tnode** rootptr, int value);
int search_node(Tnode** rootptr, int value);
void remove_node(Tnode** rootptr, int value);
void preorder_trav(Tnode* node);
void inorder_trav(Tnode* node);
void postorder_trav(Tnode* node);
void print2DUtil(struct Tnode* root, int space);
void print2D(struct Tnode* root);
int get_height(Tnode* root);