#include"structures/bst.h"


int main(){
  Tnode *tree = NULL;

  insert_node(&tree, 8);
  insert_node(&tree, 2);
  insert_node(&tree, 4);
  insert_node(&tree, 1);
  insert_node(&tree, 32);
  insert_node(&tree, 16);
  insert_node(&tree, 64);

  // remove_node(&tree, 32);
  // remove_node(&tree, 2);
  // remove_node(&tree, 17);
  print2D(tree);

  puts("\nPre-Order: ");
  preorder_trav(tree);
  puts("\nIn-Order: ");
  inorder_trav(tree);
  puts("\nPost-Order:");
  postorder_trav(tree);
  puts("\n");

  printf("Height of tree is: %d", get_height(tree));
}