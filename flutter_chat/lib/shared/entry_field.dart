import 'package:flutter/material.dart';

class EntryField extends StatelessWidget {
  EntryField({this.controller, this.textDecoration, this.isPassword});

  final TextEditingController controller;
  final String textDecoration;
  final bool isPassword;

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.symmetric(vertical: 10),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          SizedBox(
            height: 10,
          ),
          TextField(
            obscureText: isPassword,
            controller: controller,
            decoration: InputDecoration(
                labelText: textDecoration, border: BorderRadius()),
          )
        ],
      ),
    );
  }
}
