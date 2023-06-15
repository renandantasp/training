#include"node.h"

typedef struct Stack{
  Node* top;
} Stack;

void initialize_stack(Stack* stack);
void push(Stack* stack, int value);
int pop(Stack* stack);
int peek(Stack* stack);