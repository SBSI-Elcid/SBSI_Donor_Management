<template>
    <v-container fluid>
        <v-card outlined>
            <v-card-title class="headline font-weight-bold justify-center red lighten-4">
                CHECKLIST
            </v-card-title>

            <v-card-text>
                <v-form>
                    <v-row>
                        <v-col cols="12">
                            <v-card outlined>
                                <v-card-title class="red white--text py-2 px-4 font-weight-bold">
                                    CONFIRMATION CALL
                                </v-card-title>
                                <v-card-text>
                                    <v-textarea label="Input details" outlined dense></v-textarea>
                                </v-card-text>
                            </v-card>
                        </v-col>

                        <v-col cols="12">
                            <v-card outlined>
                                <v-card-title class="red white--text py-2 px-4 font-weight-bold">
                                    VERIFICATION CALL
                                </v-card-title>
                                <v-card-text>
                                    <v-textarea label="Input details" outlined dense></v-textarea>
                                </v-card-text>
                            </v-card>
                        </v-col>

                        <v-col cols="12">
                            <v-card outlined>
                                <v-card-title class="red white--text py-2 px-4 font-weight-bold">
                                    FINAL CALL
                                </v-card-title>
                                <v-card-text>
                                    <v-textarea label="Input details" outlined dense></v-textarea>
                                </v-card-text>
                            </v-card>
                        </v-col>

                        <v-col cols="12" class="text-right">
                            <v-btn color="red" dark large> SAVE </v-btn>
                        </v-col>
                    </v-row>
                </v-form>
            </v-card-text>
        </v-card>
    </v-container>
</template>