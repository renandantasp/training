#include "structures/linked_list.h"
#include "structures/stack.h"

int main(){

  Node* list = NULL;
  Node* element = NULL;
  puts("--------- Linked List Operations --------- ");
  
  insert_beginning(&list, 2);
  display_list(list);
  
  insert_beginning(&list, 1);
  display_list(list);
  
  insert_end(&list, 3);
  display_list(list);

  delete_node(&list, 3);
  display_list(list);

  delete_node(&list, 1);
  display_list(list);

  element = get_node(&list, 2);
  display_list(list);


  delete_list(&list);
  display_list(list);

  puts("------------------------------------------ ");
  puts("------------ Stack Operations ------------ ");

  Stack stack;
  initialize_stack(&stack);
  push(&stack, 4);
  push(&stack, 3);
  push(&stack, 1);
  push(&stack, 0);
  peek(&stack);
  printf("Value popped: %d\n", pop(&stack));
  printf("Value popped: %d\n", pop(&stack));
  push(&stack, 2);
  printf("Value popped: %d\n", pop(&stack));
  printf("Value popped: %d\n", pop(&stack));
  printf("Value popped: %d\n", pop(&stack));
  printf("Value popped: %d\n", pop(&stack));

  puts("------------------------------------------ ");

}