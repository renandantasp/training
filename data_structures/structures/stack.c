#include"stack.h"

void initialize_stack(Stack* stack){
  stack->top = NULL;
}

void push(Stack* stack, int value){
  Node* new_node = create_node(value);
  new_node->next = stack->top;
  stack->top =  new_node;
}

int pop(Stack* stack){
  if(stack->top == NULL){
    puts("Stack is empty");
    return -1;
  }
  Node* temp = stack->top;
  int data = temp->value;
  stack->top = stack->top->next;
  free(temp);
  return data;

}

int peek(Stack* stack){
  if(stack->top == NULL){
    puts("Stack is empty");
    return -1;
  }
  return stack->top->value;

}