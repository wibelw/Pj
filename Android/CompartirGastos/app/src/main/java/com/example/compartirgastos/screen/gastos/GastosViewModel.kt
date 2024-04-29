package com.example.compartirgastos.screen.gastos

import androidx.compose.runtime.mutableStateOf
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.compartirgastos.data.MongoDB
import com.example.compartirgastos.model.Gasto
import com.example.compartirgastos.model.Person
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import org.mongodb.kbson.ObjectId
class GastosViewModel : ViewModel() {
    var name = mutableStateOf("")
    var objectId = mutableStateOf("")
    var filtered = mutableStateOf(false)
    var data = mutableStateOf(emptyList<Gasto>())

    init {
        viewModelScope.launch {
            MongoDB.getGastos().collect {
                data.value = it
            }
        }
    }

    fun updateName(name: String) {
        this.name.value = name
    }

    fun updateObjectId(id: String) {
        this.objectId.value = id
    }

    fun insertGasto() {
        viewModelScope.launch(Dispatchers.IO) {
            if (name.value.isNotEmpty()) {
                MongoDB.insertGasto(gasto = Gasto().apply {
                    name = this@GastosViewModel.name.value
                })
            }
        }
    }

    fun updateGasto() {
        viewModelScope.launch(Dispatchers.IO) {
            if (objectId.value.isNotEmpty()) {
                MongoDB.updateGasto(gasto = Gasto().apply {
                    _id = ObjectId(hexString = this@GastosViewModel.objectId.value)
                    name = this@GastosViewModel.name.value
                })
            }
        }
    }

    fun deleteGasto() {
        viewModelScope.launch {
            if (objectId.value.isNotEmpty()) {
                MongoDB.deleteGasto(id = ObjectId(hexString = objectId.value))
            }
        }
    }

    fun filterGasto() {
        viewModelScope.launch(Dispatchers.IO) {
            if (filtered.value) {
                MongoDB.getGastos().collect {
                    filtered.value = false
                    name.value = ""
                    data.value = it
                }
            } else {
                MongoDB.filterGasto(name = name.value).collect {
                    filtered.value = true
                    data.value = it
                }
            }
        }
    }

}