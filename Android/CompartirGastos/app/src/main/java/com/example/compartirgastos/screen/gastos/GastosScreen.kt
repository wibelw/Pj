package com.example.compartirgastos.screen.gastos

import android.annotation.SuppressLint
import androidx.annotation.StringRes
import androidx.compose.foundation.horizontalScroll
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.text.selection.SelectionContainer
import androidx.compose.material3.*
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.example.compartirgastos.R
import com.example.compartirgastos.model.Person
import io.realm.kotlin.types.RealmInstant
import java.text.SimpleDateFormat
import java.util.*

import androidx.compose.ui.res.stringResource
import com.example.compartirgastos.model.Gasto
import java.time.Instant

@SuppressLint("UnusedMaterial3ScaffoldPaddingParameter")
@Composable
fun GastosScreen(
    data: List<Gasto>,
    filtered: Boolean,
    name: String,
    objectId: String,
    onNameChanged: (String) -> Unit,
    onObjectIdChanged: (String) -> Unit,
    onInsertClicked: () -> Unit,
    onUpdateClicked: () -> Unit,
    onDeleteClicked: () -> Unit,
    onFilterClicked: () -> Unit
) {
    Scaffold(
        content = {
            GastosContent(
                data = data,
                filtered = filtered,
                name = name,
                objectId = objectId,
                onNameChanged = onNameChanged,
                onObjectIdChanged = onObjectIdChanged,
                onInsertClicked = onInsertClicked,
                onUpdateClicked = onUpdateClicked,
                onDeleteClicked = onDeleteClicked,
                onFilterClicked = onFilterClicked
            )
        }
    )
}

@Composable
fun GastosContent(
    data: List<Gasto>,
    filtered: Boolean,
    name: String,
    objectId: String,
    onNameChanged: (String) -> Unit,
    onObjectIdChanged: (String) -> Unit,
    onInsertClicked: () -> Unit,
    onUpdateClicked: () -> Unit,
    onDeleteClicked: () -> Unit,
    onFilterClicked: () -> Unit
) {
    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(all = 24.dp),
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Column {
            Row {
                TextField(
                    modifier = Modifier.weight(1f),
                    value = objectId,
                    onValueChange = onObjectIdChanged,
                    placeholder = { Text(text = "Object ID") }
                )
                Spacer(modifier = Modifier.width(12.dp))
                TextField(
                    modifier = Modifier.weight(1f),
                    value = name,
                    onValueChange = onNameChanged,
                    placeholder = { Text(text = "Name") }
                )
                Spacer(modifier = Modifier.width(12.dp))

            }
            Row{
                CampoTexto(text=R.string.nom_pagador)
            }
            Row{
                CampoTexto(text=R.string.des_gasto)
            }
            Row{
                CampoTexto(text=R.string.text_importe)
            }
            Spacer(modifier = Modifier.height(24.dp))
            Row(
                modifier = Modifier
                    .fillMaxWidth()
                    .horizontalScroll(state = rememberScrollState()),
                horizontalArrangement = Arrangement.SpaceBetween
            ) {
                Button(onClick = onInsertClicked) {
                    Text(text = "Add")
                }
                Spacer(modifier = Modifier.width(6.dp))
                Button(onClick = onUpdateClicked) {
                    Text(text = "Update")
                }
                Spacer(modifier = Modifier.width(6.dp))
                Button(onClick = onDeleteClicked) {
                    Text(text = "Delete")
                }
                Spacer(modifier = Modifier.width(6.dp))
                Button(onClick = onFilterClicked) {
                    Text(text = if (filtered) "Clear" else "Filter")
                }
            }
        }
        Spacer(modifier = Modifier.height(24.dp))
        LazyColumn(modifier = Modifier.weight(1f)) {
            items(items = data, key = { it._id.toHexString() }) {
                GastoView(
                    id = it._id.toHexString(),
                    name = it.name,
                    timestamp = it.timestamp
                )
            }
        }
    }
}

@Composable
fun CampoTexto(
    modifier: Modifier = Modifier,
    @StringRes text:Int,
) {
    TextField(
        value="",
        onValueChange={},
        colors=TextFieldDefaults.colors(
            unfocusedContainerColor = MaterialTheme.colorScheme.surface,
            focusedContainerColor = MaterialTheme.colorScheme.surface
        ),
        placeholder = {
            Text(stringResource(text))
        },
        modifier= modifier
            .fillMaxWidth()
            .heightIn(min = 56.dp)
    )
}

@Composable
fun GastoView(id: String, name: String, timestamp: RealmInstant) {
    Row(modifier = Modifier.padding(bottom = 24.dp)) {
        Column(modifier = Modifier.weight(1f)) {
            Text(
                text = name,
                style = TextStyle(
                    fontSize = MaterialTheme.typography.titleLarge.fontSize,
                    fontWeight = FontWeight.Bold
                )
            )
            SelectionContainer {
                Text(
                    text = id,
                    style = TextStyle(
                        fontSize = MaterialTheme.typography.bodyMedium.fontSize,
                        fontWeight = FontWeight.Normal
                    )
                )
            }
        }
        Column(
            verticalArrangement = Arrangement.Center,
            horizontalAlignment = Alignment.End
        ) {
            Text(
                text = SimpleDateFormat("hh:mm a", Locale.getDefault())
                    .format(Date.from(timestamp.toInstant())).uppercase(),
                style = TextStyle(
                    fontSize = MaterialTheme.typography.bodyMedium.fontSize,
                    fontWeight = FontWeight.Normal
                )
            )
        }
    }
}
@Preview
@Composable
fun GastosScreenPreview(){
    val p1: Gasto=Gasto()
    p1.name="Laura"
    val list=listOf(p1)
    val name="a"
    GastosScreen(
        data = list,
        filtered = false,
        name = name,
        objectId = "1",
        onNameChanged ={} ,
        onObjectIdChanged = {},
        onInsertClicked = { /*TODO*/ },
        onUpdateClicked = { /*TODO*/ },
        onDeleteClicked = { /*TODO*/ }) {

    }
}

fun RealmInstant.toInstant(): Instant {
    val sec: Long = this.epochSeconds
    val nano: Int = this.nanosecondsOfSecond
    return if (sec >= 0) {
        Instant.ofEpochSecond(sec, nano.toLong())
    } else {
        Instant.ofEpochSecond(sec - 1, 1_000_000 + nano.toLong())
    }
}

