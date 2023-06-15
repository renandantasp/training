#include "node.h"

Node* create_node(int data){
  Node* node = (Node*) malloc(sizeof(Node));
  if (node == NULL){
    puts("Memory allocation failed.\nExiting the program");
    exit(1);
  }
  node->value = data;
  node->next = NULL;
  return node;
}