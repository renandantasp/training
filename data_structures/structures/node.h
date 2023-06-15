#ifndef NODE
#define NODE

#include<stdlib.h>
#include<stdio.h>

typedef struct Node {
  int value;
  struct Node* next;
} Node;

Node* create_node(int data);

#endif