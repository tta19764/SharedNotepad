import axios from "../api/axios";
import type { NoteType } from "../types/NoteType"

export const noteService = {
    async getNote(id: string): Promise<NoteType> {
        try {
            const response = await axios.get<{ id: string; text: string }>(`/notes/${id}`);
            return {
                id: response.data.id,
                content: response.data.text
            };
        } catch (error) {
            console.error("getNote failed", error);
            throw new Error("Failed to fetch note")
        }
    },

    async saveNote(note: NoteType): Promise<void> {
        try {
            await axios.put(`/notes/${note.id}`, {
                text: note.content
            });
        } catch (error) {
            console.error("saveNote failed", error);
            throw new Error("Failed to save note")
        }
    },

    async createNote(): Promise<string> {
        try {
            const response = await axios.post<{ id: string }>('/notes');
            return response.data.id;
        } catch (error) {
            console.error("createNote failed", error);
            throw new Error("Failed to create note")
        }
    },

    async deleteNote(id: string): Promise<void> {
        try {
            await axios.delete(`/notes/${id}`);
        } catch (error) {
            console.error("deleteNote failed", error);
            throw new Error("Failed to delete note")
        }
    }
};
