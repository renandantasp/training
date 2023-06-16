#include"structures/bst.h"


int main(){
  Tnode *tree = NULL;

  insert_node(&tree, 8);
  insert_node(&tree, 4);
  insert_node(&tree, 2);
  insert_node(&tree, 32);
  insert_node(&tree, 16);
  insert_node(&tree, 64);

  // remove_node(&tree, 32);
  // remove_node(&tree, 2);
  // remove_node(&tree, 17);
  print2D(tree);
  puts("\n");
  preorder_trav(tree);
  puts("\n");
  inorder_trav(tree);
  puts("\n");
  postorder_trav(tree);
  puts("\n");

}